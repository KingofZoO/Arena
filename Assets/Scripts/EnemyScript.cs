using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : UnitScript
{
    private void Awake()
    {
        SetParameters();
    }

    protected override void SetParameters()
    {
        actionPoints = 3;
        RefreshAP();
        damagePoints = 3;
        healthPoints = 10;
    }
}
