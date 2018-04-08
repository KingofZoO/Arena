using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    protected int actionPoints;
    protected int currActionPoints;

    public void GetAction()
    {
        currActionPoints -= 1;
    }

    public void RefreshAP()
    {
        currActionPoints = actionPoints;
    }

    public int CurrentActionPoints
    {
        get { return currActionPoints; }
    }
}
