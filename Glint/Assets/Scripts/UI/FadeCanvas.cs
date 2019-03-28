using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using utils;

public class FadeCanvas : MonoBehaviour
{
    private static Image fadePanel;
    private static FadeCanvas instance;
    private static Ref<Color32> refColor;

    public static FadeCanvas getInstance()
    {
        if (FadeCanvas.instance == null)
        {
            GameObject FadeCanvasGO = (GameObject)MonoBehaviour.Instantiate(Resources.Load("UI/Fade Canvas"));
            FadeCanvas.fadePanel = FadeCanvasGO.transform.GetChild(0).GetComponent<Image>();
            FadeCanvas.instance = FadeCanvasGO.GetComponent<FadeCanvas>();
        }

        return FadeCanvas.instance;
    }

    public void FadeIn(float fadeDuration)
    {
        this.StartCoroutine(FadeFunc.DoFade((Color32 color) => FadeCanvas.fadePanel.color = color, 255, 0, fadeDuration));
    }

    public void FadeOut(float fadeDuration)
    {
        this.StartCoroutine(FadeFunc.DoFade((Color32 color) => FadeCanvas.fadePanel.color = color, 0, 255, fadeDuration));
    }
}