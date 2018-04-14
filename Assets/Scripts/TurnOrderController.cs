using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOrderController : MonoBehaviour
{
    private Transform unit;
    private List<Transform> units = new List<Transform>();
    private int currUnit = 0;

    private void Start()
    {
        units = MainScript.Instance.GetUnitGeneratorScript.GetUnitList;
    }

    public Transform GetWalkingUnit()
    {
        if (currUnit >= units.Count)
            currUnit = 0;

        unit = units[currUnit];
        currUnit++;

        return unit;
    }

    public Transform GetNearUnit()
    {
        Transform nearUnit = null;
        float dist = Mathf.Infinity;

        for (int i = 0; i < units.Count; i++)
        {
            if (units[i] != unit && Vector3.Distance(units[i].position, unit.position) < dist)
            {
                dist = Vector3.Distance(units[i].position, unit.position);
                nearUnit = units[i];
            }
        }
        if (nearUnit == null)
            return unit;

        return nearUnit;
    }

    public void KillTarget(Transform targetUnit)
    {
        var index = units.IndexOf(targetUnit);
        units.RemoveAt(index);
        targetUnit.gameObject.SetActive(false);

        if (index < currUnit)
            currUnit--;
    }

    public Transform CurrentUnit
    {
        get { return unit; }
    }
}
