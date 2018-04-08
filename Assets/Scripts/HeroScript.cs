using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroScript : UnitScript
{
    private void Awake()
    {
        actionPoints = 4;
        RefreshAP();
    }
}
