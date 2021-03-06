﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using utils;

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

    public float duration;

    void Awake()
    {
        this.image = this.GetComponent<Image>();
        this.baseColor = this.image.color;
    }

    public IEnumerator FadeAnimation()
    {
        Opacity = 0;
        yield return StartCoroutine(utils.Coroutine.WaitForRealSeconds(this.startTime));
        StartCoroutine(FadeFunc.DoFadeOpacity((opacity) => this.Opacity = opacity, 0f, 1f, this.duration));
    }
}
