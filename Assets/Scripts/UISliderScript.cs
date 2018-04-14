using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISliderScript : MonoBehaviour
{
    [SerializeField] private Text valueText;

    public void UnitCountSliderChange()
    {
        valueText.text = UIStartPanelScript.Instance.UnitCountSlider.value.ToString();
        UIStartPanelScript.Instance.AICountSlider.maxValue = UIStartPanelScript.Instance.UnitCountSlider.value;
    }

    public void AICountSliderChange()
    {
        valueText.text = UIStartPanelScript.Instance.AICountSlider.value.ToString();
    }
}
