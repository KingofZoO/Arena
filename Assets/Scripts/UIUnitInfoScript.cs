using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIUnitInfoScript : MonoBehaviour
{
    protected Text actionPoints;
    protected Text damagePoints;
    protected Text healthPoints;
    protected Text unitName;

    protected Transform unit;
    protected UnitScript unitScript;

    protected virtual void Awake()
    {
        actionPoints = transform.Find("APLabel").GetComponent<Text>();
        damagePoints = transform.Find("DPLabel").GetComponent<Text>();
        healthPoints = transform.Find("HPLabel").GetComponent<Text>();
        unitName = transform.Find("UnitName").GetComponent<Text>();
    }

    protected virtual void Update()
    {
        if (Time.frameCount % 10 == 0)
            SetValues();
    }

    protected abstract void SetValues();
}
