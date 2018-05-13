using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelController : MonoBehaviour
{
    public static PanelController Instance { get; private set; }

    [SerializeField] private UIPauseMenuScript pauseMenu;
    [SerializeField] private UIUnitPanelScript unitPanel;
    [SerializeField] private UITargetPanelScript targetPanel;

    private void Awake()
    {
        Instance = this;

        pauseMenu.gameObject.SetActive(false);
        unitPanel.gameObject.SetActive(true);
        targetPanel.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            pauseMenu.ShowPauseMenu();
    }

    public UIUnitPanelScript GetUnitPanel
    {
        get { return unitPanel; }
    }

    public UITargetPanelScript GetTargetPanel
    {
        get { return targetPanel; }
    }


    public UIPauseMenuScript GetPauseMenu
    {
        get { return pauseMenu; }
    }
}
