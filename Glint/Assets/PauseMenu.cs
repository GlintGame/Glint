using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Luminosity.IO;

public class PauseMenu : MonoBehaviour {

    private static PauseMenu instance;

    private bool gameIsPaused = false;
    private bool waitForButtonRelease = false;

    private GameObject PauseMenuUI;
    private Button FocusButton;
    private EventSystem eventSystem;

    public delegate void PauseAction();
    public static event PauseAction OnPause;

    public delegate void ResumeAction();
    public static event ResumeAction OnResume;

    public delegate void ResumeEscapeAction();
    public static event ResumeEscapeAction OnResumeEscape;

    public delegate void ReleaseButtonAction();
    public static event ReleaseButtonAction OnReleaseButton;

    void Awake()
    {
        if (PauseMenu.instance == null)
        {
            PauseMenu.instance = this;

            GameObject.DontDestroyOnLoad(this);
            PauseMenu.OnPause += this.Focus;
            SceneManager.sceneLoaded += this.GetMenuElements;
            PauseMenu.OnReleaseButton += this.Useless;
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }



    void Update() {
        if (InputManager.GetButtonDown("UI_Menu"))
        {
            if(this.gameIsPaused)
            {
                this.Resume();

                if (OnResumeEscape != null)
                    OnResumeEscape();
            }
            else
            {
                this.Pause();
            }
        }

        if(this.waitForButtonRelease
            && InputManager.GetButtonUp("UI_Submit"))
        {
            this.waitForButtonRelease = false;

            if (OnReleaseButton != null)
                OnReleaseButton();
        }
	}




    void Pause()
    {
        this.PauseMenuUI.SetActive(true);
        this.gameIsPaused = true;
        Time.timeScale = 0f;

        if (OnPause != null)
            OnPause();
    }

    public void Resume()
    {
        this.PauseMenuUI.SetActive(false);
        this.gameIsPaused = false;
        Time.timeScale = 1f;

        if (OnResume != null)
            OnResume();
    }

    public void StartWaiting()
    {
        this.waitForButtonRelease = true;
    }





    private void Focus()
    {
        this.eventSystem.SetSelectedGameObject(this.PauseMenuUI);
        this.FocusButton.Select();
    }




    private void GetMenuElements(Scene scene, LoadSceneMode loadSceneMode)
    {
        this.eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        this.PauseMenuUI = this.gameObject.transform.Find("Pause Menu").gameObject;
        this.FocusButton = this.PauseMenuUI.transform.Find("Resume").gameObject.GetComponent<Button>();
    }

    private void Useless()
    {
        Debug.Log("OK");
    }
}
