using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Luminosity.IO;

public class PauseMenu : MonoBehaviour {

    public bool isScenePausable = false;
    private bool gameIsPaused = false;
    private bool waitForButtonRelease = false;

    public GameObject PauseMenuUI;
    public Button FocusButton;
    public EventSystem eventSystem;

    public delegate void PauseAction();
    public static event PauseAction OnPause;

    public delegate void ResumeAction();
    public static event ResumeAction OnResume;

    public delegate void ResumeEscapeAction();
    public static event ResumeEscapeAction OnResumeEscape;

    public delegate void ReleaseButtonAction();
    public static event ReleaseButtonAction OnReleaseButton;

    void Update() {

        if (InputManager.GetButtonDown("UI_Menu") && this.isScenePausable)
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
        Time.fixedDeltaTime = 0f;

        this.Focus();

        if (OnPause != null)
            OnPause();
    }

    public void Resume()
    {
        this.PauseMenuUI.SetActive(false);
        this.gameIsPaused = false;

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        if (OnResume != null)
            OnResume();
    }

    public void Focus()
    {
        this.eventSystem.SetSelectedGameObject(this.PauseMenuUI);
        this.FocusButton.Select();
    }



    public void StartWaiting()
    {
        this.waitForButtonRelease = true;
    }
}
