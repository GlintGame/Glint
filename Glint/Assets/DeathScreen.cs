using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using utils;

public class DeathScreen : SelectableScreen
{
    public GameObject canvas;
    public Image panel;
    public TextMeshProUGUI deathText;
    public Image deathImage;
    public TextMeshProUGUI retryButtonText;
    public TextMeshProUGUI giveupButtonText;

    Color32 whiteT = new Color32(255, 255, 255, 0);
    Color32 blackT = new Color32(0, 0, 0, 0);

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
        this.SetBaseColors();

        StartCoroutine(this.ShowScreen());
    }

    public override void Desactivate()
    {
        base.RemoveFromActiveScreens(this);

        this.SetBaseColors();
        this.canvas.SetActive(false);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    public override void Focus()
    {
        this.eventSystem.SetSelectedGameObject(this.canvas);
        this.focusButton.Select();
    }

    private void SetBaseColors()
    {
        this.panel.color = blackT;
        this.deathText.color = whiteT;
        this.deathImage.color = whiteT;
        this.retryButtonText.color = whiteT;
        this.giveupButtonText.color = whiteT;
    }

    private IEnumerator ShowScreen()
    {
        yield return StartCoroutine(FadeFunc.DoFadeOutBlack((Color32 color) => { this.panel.color = color; }, 1.5f));
        yield return StartCoroutine(utils.Coroutine.WaitForRealSeconds(1.0f));
        StartCoroutine(FadeFunc.DoFadeOutWhite((Color32 color) => { this.deathText.color = color; }, 1f));
        yield return StartCoroutine(FadeFunc.DoFade((Color32 color) => { this.deathImage.color = color; }, whiteT, new Color32(255, 255, 255, 75), 1f));
        yield return StartCoroutine(utils.Coroutine.WaitForRealSeconds(0.5f));
        yield return StartCoroutine(FadeFunc.DoFadeOutWhite((Color32 color) => { this.retryButtonText.color = color; }, 0.5f));
        yield return StartCoroutine(utils.Coroutine.WaitForRealSeconds(0.3f));
        yield return StartCoroutine(FadeFunc.DoFadeOutWhite((Color32 color) => { this.giveupButtonText.color = color; }, 0.5f));
        yield return StartCoroutine(utils.Coroutine.WaitForRealSeconds(0.1f));

        this.Focus();
        yield return null;
    }
}
