using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUnitPanelScript : UIUnitInfoScript
{
    protected override void SetValues()
    {
        unit = MainScript.Instance.GetTurnOrderController.CurrentUnit;

        if (unit == null)
            return;

        unitScript = unit.GetComponent<UnitScript>();

        if (unit.tag == "Hero")
        {
            unitName.text = unit.name;
            actionPoints.text = unitScript.CurrentActionPoints.ToString();
            damagePoints.text = unitScript.DamagePoints.ToString();
            healthPoints.text = unitScript.HealthPoints.ToString();
        }
        else
        {
            unitName.text = null;
            actionPoints.text = null;
            damagePoints.text = null;
            healthPoints.text = null;
        }
    }
}
