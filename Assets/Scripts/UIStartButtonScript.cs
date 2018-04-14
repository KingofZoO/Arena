using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartButtonScript : MonoBehaviour
{
    public void StartGame()
    {
        MainScript.Instance.GetArenaGeneratorScript.GenerateArena();
        MainScript.Instance.GetUnitGeneratorScript.GenerateUnits((int)UIStartPanelScript.Instance.UnitCountSlider.value, (int)UIStartPanelScript.Instance.AICountSlider.value, UIStartPanelScript.Instance.BattleTypeToggle.isOn);

        transform.parent.gameObject.SetActive(false);
    }
}
