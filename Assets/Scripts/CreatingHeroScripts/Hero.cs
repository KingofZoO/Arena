using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero
{
    public Sprite heroSprite;
    public string heroName;

    private int actionPoints;
    private int damagePoints;
    private int healthPoints;

    private int basicAP = 4;
    private int basicDP = 4;
    private int basicHP = 10;

    public List<int> GetCurrentStats()
    {
        return new List<int>
        {
            actionPoints,
            damagePoints,
            healthPoints
        };
    }

    public List<int> GetBasicStats()
    {
        return new List<int>
        {
            basicAP,
            basicDP,
            basicHP
        };
    }

    public void SetCurrentStats(List<int> stats)
    {
        actionPoints = stats[0];
        damagePoints = stats[1];
        healthPoints = stats[2];
    }

    public void ResetStats()
    {
        actionPoints = basicAP;
        damagePoints = basicDP;
        healthPoints = basicHP;
    }
}
