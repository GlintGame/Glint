using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour, IFadeable
{
    private Image image;
    private Color32 baseColor;

    public float Opacity
    {
        get { return (float)this.image.color.a / 255; }
        set { this.image.color = new Color32(this.baseColor.r, this.baseColor.g, this.baseColor.b, (byte)(value * 255)); }
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
        this.image = this.GetComponent<Image>();
        this.baseColor = this.image.color;
    }
}
