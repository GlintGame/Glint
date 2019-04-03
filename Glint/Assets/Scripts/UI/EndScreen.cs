using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : SelectableScreen
{
    public GameObject canvas;
    public FadeAnimator fadeAnimator;

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
