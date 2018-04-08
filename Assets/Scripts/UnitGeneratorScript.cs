using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGeneratorScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> Hero;
    [SerializeField] private List<GameObject> Enemy;

    private float widthHexDist;
    private float heightHexDist;
    private int ringCount;

    private void Start()
    {
        var arena = GetComponent<ArenaGeneratorScript>();
        widthHexDist = arena.WidthDistBtwHex;
        heightHexDist = arena.HeightDistBtwHex;
        ringCount = arena.RingCount;

        SetUnits();
    }

    private void SetUnits()
    {
        Instantiate(Hero[0], Vector2.zero, Quaternion.identity);
        Instantiate(Hero[1], new Vector2(widthHexDist * ringCount, 0), Quaternion.identity);
        Instantiate(Hero[2], new Vector2(-widthHexDist * ringCount, 0), Quaternion.identity);

        for(int i = 1; i <= Enemy.Count; i++)
        {
            if (i % 2 == 0)
                Instantiate(Enemy[i-1], new Vector2(0, heightHexDist * (i-1)), Quaternion.identity);
            else Instantiate(Enemy[i-1], new Vector2(0, -heightHexDist * (i)), Quaternion.identity);
        }
    }
}
