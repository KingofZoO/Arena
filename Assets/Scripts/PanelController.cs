using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelController : MonoBehaviour
{
    public static PanelController Instance { get; private set; }

    [SerializeField] private UIStartPanelScript startMenu;
    [SerializeField] private UIPauseMenuScript pauseMenu;
    [SerializeField] private UIUnitPanelScript unitPanel;
    [SerializeField] private UITargetPanelScript targetPanel;

    private void Awake()
    {
        Instance = this;

        startMenu.gameObject.SetActive(true);
        pauseMenu.gameObject.SetActive(false);
        unitPanel.gameObject.SetActive(false);
        targetPanel.gameObject.SetActive(false);
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
}
