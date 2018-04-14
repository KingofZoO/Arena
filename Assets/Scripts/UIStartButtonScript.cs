using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartButtonScript : MonoBehaviour
{
    public void StartGame()
    {
        MainScript.Instance.GetArenaGeneratorScript.GenerateArena();
        MainScript.Instance.GetUnitGeneratorScript.GenerateUnits((int)UIStartPanelScript.Instance.UnitCountSlider.value, (int)UIStartPanelScript.Instance.AICountSlider.value, UIStartPanelScript.Instance.BattleTypeToggle.isOn);

        PanelController.Instance.GetUnitPanel.gameObject.SetActive(true);
        PanelController.Instance.GetTargetPanel.gameObject.SetActive(true);

        transform.parent.gameObject.SetActive(false);
    }
}
