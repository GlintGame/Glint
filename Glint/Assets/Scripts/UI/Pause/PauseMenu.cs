using UnityEngine;
using Luminosity.IO;

public class PauseMenu : SelectableScreen {

    public bool isScenePausable = false;
    private bool waitForButtonRelease = false;

    public GameObject PauseMenuUI;

    public delegate void PauseAction();
    public static event PauseAction OnPause;

    public delegate void ResumeAction();
    public static event ResumeAction OnResume;

    public delegate void ResumeEscapeAction();
    public static event ResumeEscapeAction OnResumeEscape;

    public delegate void ReleaseButtonAction();
    public static event ReleaseButtonAction OnReleaseButton;

    void Update() {

        //base.DedugAll();

        if (InputManager.GetButtonDown("UI_Menu")
            && this.isScenePausable
            && !base.AnotherIsActive(this))
        {
            if(this.isActive)
            {
                this.Desactivate();

                if (OnResumeEscape != null)
                    OnResumeEscape();
            }
            else
            {
                this.Activate();
            }
        }

        // Invoke an event when the submit button is released
        // this is in the case that the submit button also trigger another action like jumping
        if(this.waitForButtonRelease
            && InputManager.GetButtonUp("UI_Submit"))
        {
            this.waitForButtonRelease = false;

            if (OnReleaseButton != null)
                OnReleaseButton();
        }
	}


    override public void Activate()
    {
        base.AddToActiveScreens(this);

        this.PauseMenuUI.SetActive(true);
        this.isActive = true;

        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0f;
        
        this.Focus();

        if (OnPause != null)
            OnPause();
    }

    override public void Desactivate()
    {
        base.RemoveFromActiveScreens(this);

        this.PauseMenuUI.SetActive(false);
        this.isActive = false;

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        
        if (OnResume != null)
            OnResume();
    }

    public override void Focus()
    {
        this.eventSystem.SetSelectedGameObject(this.PauseMenuUI);
        this.focusButton.Select();
    }



    public void StartWaiting()
    {
        this.waitForButtonRelease = true;
    }
}
