﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaGeneratorScript : MonoBehaviour
{
    [SerializeField] private GameObject Hex;

    private GameObject arena;
    private SpriteRenderer hexSprite;

    private float hexHalfWidth;
    private float hexHalfHeight;

    private float sizingCorr = 0.05f;
    private int nameCount = -1;

    private int triangRowCount = 5;
    [SerializeField] private int ringCount = 6;

    private void Awake()
    {
        StartPreparation();
        GenerateHexagonMap();
    }

    private void GenerateTriangleMap()
    {
        for (int j = 0; j < triangRowCount; j++)
        {
            var posY = -1.5f * hexHalfHeight * j;
            var posX = -hexHalfWidth * j;

            for (int i = 0; i < j + 1; i++)
            {
                var pos = new Vector2(posX + 2 * hexHalfWidth * i, posY);
                CreateHex(pos, j);
            }
        }
    }

    private void GenerateHexagonMap()
    {
        CreateHex(Vector2.zero, 0);

        for (int j = 1; j <= ringCount; j++)
        {
            Vector2 pos = new Vector2(2 * hexHalfWidth * j, 0);
            Vector2 delt = new Vector2(2 * hexHalfWidth, 0);
            float angle = 120f;

            for (int i = 0; i < 6; i++)
            {
                delt = Quaternion.Euler(0, 0, angle) * delt;
                for (int k = 0; k < j; k++)
                {
                    CreateHex(pos, j);
                    pos += delt;
                }
                angle = 60f;
            }
        }
    }

    private void CreateHex(Vector2 pos, int currentRing)
    {
        nameCount++;
        var temp = Instantiate(Hex, pos, Quaternion.identity);
        temp.name = Hex.name + "(" + nameCount + ")";
        temp.transform.parent = arena.transform;
    }

    private void CalcHalfWidth()
    {
        hexHalfWidth = hexSprite.bounds.size.x / 2f - sizingCorr;
    }

    private void CalcHalfHeight()
    {
        hexHalfHeight = hexSprite.bounds.size.y / 2f - sizingCorr;
    }

    private void StartPreparation()
    {
        arena = new GameObject("Arena");
        arena.AddComponent<ArenaPathScript>();
        arena.AddComponent<UnitMovingScript>();
        hexSprite = Hex.GetComponent<SpriteRenderer>();
        CalcHalfHeight();
        CalcHalfWidth();
    }

    public float WidthDistBtwHex
    {
        get { return 2 * hexHalfWidth; }
    }

    public float HeightDistBtwHex
    {
        get { return 3 * hexHalfHeight; }
    }

    public int RingCount
    {
        get { return ringCount; }
    }
}
