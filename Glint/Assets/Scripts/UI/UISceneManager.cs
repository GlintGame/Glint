using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UISceneManager : MonoBehaviour {

    private static UISceneManager instance;
    private GameObject UIPack;

    void Awake()
    {
        this.UIPack = GameObject.FindGameObjectWithTag("UIPack");

        if (UISceneManager.instance == null)
        {
            UISceneManager.instance = this;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    /*
    public void CloseMainMenu(string sceneName)
    {
        this.pauseMenu = UIPack.GetComponent<PauseMenu>();
        this.mainMenu.SetActive(false);
        this.pauseMenu.isScenePausable = false;

        SceneManager.LoadScene(sceneName);
    }

    public void OpenMainMenu()
    {
        this.pauseMenu = UIPack.GetComponent<PauseMenu>();
        this.mainMenu.SetActive(true);
        this.pauseMenu.isScenePausable = true;

        this.pauseMenu.Resume();

        SceneManager.LoadScene(mainMenuScene);
        this.selectedButtonMainMenu.Select();
    }

    public void OpenSettings()
    {
        this.lastSelectGO = this.eventSystem.currentSelectedGameObject;
        this.settingsCanvas.SetActive(true);
        this.selectedButtonSettings.Select();
    }

    public void CloseSettings()
    {
        this.settingsCanvas.SetActive(false);
        this.eventSystem.SetSelectedGameObject(this.lastSelectGO);
    }*/
}
