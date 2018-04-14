using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroScript : UnitScript
{
    private void Awake()
    {
        SetParameters();
    }

    protected override void SetParameters()
    {
        actionPoints = 4;
        RefreshAP();
        damagePoints = 4;
        healthPoints = 10;
    }
}
