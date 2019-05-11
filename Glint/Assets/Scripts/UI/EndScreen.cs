using System.Collections;
using UnityEngine;

public class EndScreen : SelectableScreen
{
    public GameObject canvas;
    public FadeAnimator fadeAnimator;
    public float timeBeforeFocus = 5f;

    public override void Activate()
    {
        base.AddToActiveScreens(this);

        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0f;
        this.canvas.SetActive(true);

        this.fadeAnimator.AllFadeIn();
        StartCoroutine(this.doFocus());
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

    IEnumerator doFocus()
    {
        yield return StartCoroutine(utils.Coroutine.WaitForRealSeconds(this.timeBeforeFocus));
        this.Focus();
    }
}
