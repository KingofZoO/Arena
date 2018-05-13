using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexColorChangeScript : MonoBehaviour, IColoredHex
{
    private SpriteRenderer spriteRenderer;

    private Color basicColor;
    private Color highlightColor = new Color(1f, 1f, 1f, 1f);
    private Color pathColor = new Color(1f, 1f, 1f, 1f);

    private bool isHighlighted = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        basicColor = spriteRenderer.color;
    }

    public void SelectHex()
    {
        SetToHighlightColor();
        isHighlighted = true;
    }

    public void DeselectHex()
    {
        isHighlighted = false;
        SetToBasicColor();
    }

    public void SetToBasicColor()
    {
        spriteRenderer.color = basicColor;
    }

    public void SetToHighlightColor()
    {
        spriteRenderer.color = highlightColor;
    }

    public void SetToPathColor()
    {
        spriteRenderer.color = pathColor;
    }

    public bool IsHighlighted
    {
        get { return isHighlighted; }
    }
}
