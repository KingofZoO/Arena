using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : UnitScript
{
    private void Awake()
    {
        actionPoints = 3;
        RefreshAP();
    }
}
