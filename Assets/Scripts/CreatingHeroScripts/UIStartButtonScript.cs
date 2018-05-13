using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartButtonScript : MonoBehaviour
{
    [SerializeField] private CreatingHeroScript creatingHeroPanel;

    public void StartCreating()
    {
        creatingHeroPanel.gameObject.SetActive(true);
        transform.parent.gameObject.SetActive(false);

        HeroDataContainer.Instance.SetHeroArray((int)UIStartPanelScript.Instance.UnitCountSlider.value,
            (int)UIStartPanelScript.Instance.AICountSlider.value);
    }
}
