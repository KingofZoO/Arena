using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITargetPanelScript : UIUnitInfoScript
{
    private LayerMask unitLayer;

    protected override void Awake()
    {
        base.Awake();
        unitLayer = 1 << LayerMask.NameToLayer("Unit");
    }

    protected override void SetValues()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, unitLayer);
        if (hit.collider)
            unit = hit.transform;
        else if (unit == null)
            return;

        unitScript = unit.GetComponent<UnitScript>();

        if (unit.tag == "Hero" && hit.collider)
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
