using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UISceneManager : MonoBehaviour {

    public string mainMenuScene = "MainMenu";

    public EventSystem eventSystem;
    public GameObject UIPack;
    public GameObject settingsCanvas;
    public Selectable selectedButtonSettings;
    public GameObject mainMenu;
    public Selectable selectedButtonMainMenu;

    private PauseMenu pauseMenu;
    private GameObject lastSelectGO;

    public void ExitGame()
    {
        Application.Quit();
    }


    public void CloseMainMenu(string sceneName)
    {
        this.pauseMenu = UIPack.GetComponent<PauseMenu>();
        this.mainMenu.SetActive(false);
        this.pauseMenu.onMainMenu = false;

        SceneManager.LoadScene(sceneName);
    }

    public void OpenMainMenu()
    {
        this.pauseMenu = UIPack.GetComponent<PauseMenu>();
        this.mainMenu.SetActive(true);
        this.pauseMenu.onMainMenu = true;

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
    }
}
