using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStartPanelScript : MonoBehaviour
{
    private Slider unitCountSlider;
    private Slider aiCountSlider;
    private Toggle battleTypeToggle;

    public static UIStartPanelScript Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        unitCountSlider = transform.Find("UnitCountSlider").GetComponent<Slider>();
        aiCountSlider = transform.Find("AICountSlider").GetComponent<Slider>();
        battleTypeToggle = transform.Find("BattleTypeToggle").GetComponent<Toggle>();

        UIElmentsSetup();
    }

    private void UIElmentsSetup()
    {
        unitCountSlider.minValue = 2;
        unitCountSlider.maxValue = 12;
        unitCountSlider.value = 2;
        unitCountSlider.wholeNumbers = true;

        aiCountSlider.minValue = 0;
        aiCountSlider.maxValue = 2;
        aiCountSlider.value = 0;
        aiCountSlider.wholeNumbers = true;
    }

    public Slider UnitCountSlider
    {
        get { return unitCountSlider; }
    }

    public Slider AICountSlider
    {
        get { return aiCountSlider; }
    }

    public Toggle BattleTypeToggle
    {
        get { return battleTypeToggle; }
    }
}
