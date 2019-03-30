using UnityEngine;

public class FadeCanvasGroup : MonoBehaviour, IFadeable
{
    private CanvasGroup canvasGroup;

    public float Opacity
    {
        get { return this.canvasGroup.alpha; }
        set { this.canvasGroup.alpha = value; }
    }

    public float startTime;
    public float StartTime
    {
        get { return startTime; }
    }

    public float duration;
    public float Duration
    {
        get { return duration; }
    }

    void Awake()
    {
        this.canvasGroup = this.GetComponent<CanvasGroup>();
    }
}
