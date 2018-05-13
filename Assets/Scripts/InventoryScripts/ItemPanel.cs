using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    private Text actionPoints;
    private Text damagePoints;
    private Text healthPoints;
    private Text itemName;

    private void Awake()
    {
        SetUIElements();
    }

    private void SetUIElements()
    {
        actionPoints = transform.Find("APLabel").GetComponent<Text>();
        damagePoints = transform.Find("DPLabel").GetComponent<Text>();
        healthPoints = transform.Find("HPLabel").GetComponent<Text>();
        itemName = transform.Find("ItemName").GetComponent<Text>();

        ResetValues();
    }

    public void SetValues(Item item)
    {
        actionPoints.text = item.ActionPoints.ToString();
        damagePoints.text = item.DamagePoints.ToString();
        healthPoints.text = item.HealthPoints.ToString();
        itemName.text = item.itemName.ToString();
    }

    public void ResetValues()
    {
        actionPoints.text = null;
        damagePoints.text = null;
        healthPoints.text = null;
        itemName.text = null;
    }
}
