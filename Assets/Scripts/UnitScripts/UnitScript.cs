using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    private int actionPoints;
    private int currActionPoints;

    private int damagePoints;
    private int healthPoints;

    public void SetParameters(Hero hero)
    {
        var stats = hero.GetCurrentStats();
        actionPoints = stats[0];
        damagePoints = stats[1];
        healthPoints = stats[2];

        RefreshAP();
    }

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
