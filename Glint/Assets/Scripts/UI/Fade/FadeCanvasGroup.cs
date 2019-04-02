using System.Collections;
using UnityEngine;
using utils;

public class FadeCanvasGroup : MonoBehaviour, IFadeable
{
    private CanvasGroup canvasGroup;

    public float Opacity
    {
        get { return this.canvasGroup.alpha; }
        set { this.canvasGroup.alpha = value; }
    }

    public float startTime;

    public float duration;

    void Awake()
    {
        this.canvasGroup = this.GetComponent<CanvasGroup>();
    }

    public IEnumerator FadeAnimation()
    {
        Opacity = 0;
        yield return StartCoroutine(utils.Coroutine.WaitForRealSeconds(this.startTime));
        StartCoroutine(FadeFunc.DoFadeOpacity((opacity) => this.Opacity = opacity, 0f, 1f, this.duration));
    }
}
