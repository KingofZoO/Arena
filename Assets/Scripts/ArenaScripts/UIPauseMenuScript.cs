using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPauseMenuScript : MonoBehaviour
{
    public void ShowPauseMenu()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }

    public void NewGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("CreatingHeroScene");
    }

    public void Continue()
    {
        Time.timeScale = 1;
        transform.parent.gameObject.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
