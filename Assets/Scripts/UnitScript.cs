using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitScript : MonoBehaviour
{
    protected int actionPoints;
    protected int currActionPoints;

    protected int damagePoints;
    public int healthPoints;

    protected abstract void SetParameters();

    public void GetAction(int actionCost)
    {
        currActionPoints -= actionCost;
    }

    public void RefreshAP()
    {
        currActionPoints = actionPoints;
    }

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
    }

    public int CurrentActionPoints
    {
        get { return currActionPoints; }
    }

    public int DamagePoints
    {
        get { return damagePoints; }
    }

    public int HealthPoints
    {
        get { return healthPoints; }
    }
}
