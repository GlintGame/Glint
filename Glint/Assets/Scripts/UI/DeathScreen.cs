using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using utils;

public class DeathScreen : SelectableScreen
{
    public GameObject canvas;
    public FadeAnimator fadeAnimator;

    public void CheckDeath(int curHealth, int maxHealth)
    {
        if (curHealth <= 0)
        { 
            this.Activate();
        }
    }

    public override void Activate() 
    {
        base.AddToActiveScreens(this);

        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0f;
        this.canvas.SetActive(true);

        this.fadeAnimator.AllFadeIn();
        this.Focus();
    }

    public override void Desactivate()
    {
        base.RemoveFromActiveScreens(this);

        this.canvas.SetActive(false);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    public override void Focus()
    {
        this.eventSystem.SetSelectedGameObject(this.canvas);
        this.focusButton.Select();
    }
}
