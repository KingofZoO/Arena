using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        return unit = (IsUnitActive(unit) == true) ? unit : GetWalkingUnit();
    }

    private bool IsUnitActive(Transform checkedUnit)
    {
        return (checkedUnit.gameObject.activeInHierarchy == true) ? true : false;
    }

    public Transform GetNearUnit()
    {
        Transform nearUnit = null;
        float dist = Mathf.Infinity;

        for (int i = 0; i < units.Count; i++)
        {
            if (units[i] != unit && IsUnitActive(units[i]) && Vector3.Distance(units[i].position, unit.position) < dist)
            {
                dist = Vector3.Distance(units[i].position, unit.position);
                nearUnit = units[i];
            }
        }
        if (nearUnit == null)
            return null;

        return nearUnit;
    }

    public void KillTarget(Transform targetUnit)
    {
        targetUnit.gameObject.SetActive(false);

        var alifeCount = 0;
        foreach (var hero in units)
            if (hero.gameObject.activeInHierarchy)
                alifeCount++;
        if (alifeCount == 1)
            StartCoroutine(NextRound());
    }

    public Transform CurrentUnit
    {
        get { return unit; }
    }

    private IEnumerator NextRound()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("InventoryScene");
    }
}
