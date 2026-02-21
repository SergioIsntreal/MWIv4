using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject playPanel;
    public GameObject levelSelectPanel;
    public GameObject quitConfirmation;

    public void Start()
    {
        levelSelectPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        quitConfirmation.SetActive(false);
        playPanel.SetActive(false);
    }

    public void OpenPlayPanel()
    {
        mainMenuPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        quitConfirmation.SetActive(false);

        playPanel.SetActive(true);
    }

    public void OpenLevelSelect()
    {
        mainMenuPanel.SetActive(false);
        playPanel.SetActive(false);
        quitConfirmation.SetActive(false);

        levelSelectPanel.SetActive(true);
    }

    public void BackToMain()
    {
        levelSelectPanel.SetActive(false);
        playPanel.SetActive(false);
        quitConfirmation.SetActive(false);

        mainMenuPanel.SetActive(true);
    }

    public void BackToPlay()
    {
        levelSelectPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        quitConfirmation.SetActive(false);

        playPanel.SetActive(true);
    }

    // Later, change this to open the 'Introduction Cutscene'
    public void NewGame()
    {
        SceneManager.LoadScene("Visual Novel Test");
    }

    public void OpenQuitConfirmation()
    {
        mainMenuPanel.SetActive(false);
        quitConfirmation.SetActive(true);
    }

    public void CloseQuitConfirmation()
    {
        quitConfirmation.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("See you next time.");
    }
}
