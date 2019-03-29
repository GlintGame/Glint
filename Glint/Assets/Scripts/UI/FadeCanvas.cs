using UnityEngine;
using UnityEngine.UI;
using utils;

public class FadeCanvas : MonoBehaviour
{
    private static Image fadePanel;
    private static FadeCanvas instance;

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
        this.StartCoroutine(FadeFunc.DoFadeInBlack((Color32 color) => FadeCanvas.fadePanel.color = color, fadeDuration));
    }

    public void FadeOut(float fadeDuration)
    {
        this.StartCoroutine(FadeFunc.DoFadeOutBlack((Color32 color) => FadeCanvas.fadePanel.color = color, fadeDuration));
    }
}