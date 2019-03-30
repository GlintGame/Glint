using UnityEngine;
using UnityEngine.UI;
using utils;

public class TransitionCanvas : MonoBehaviour
{
    private static Image fadePanel;
    private static TransitionCanvas instance;

    public static TransitionCanvas getInstance()
    {
        if (TransitionCanvas.instance == null)
        {
            GameObject FadeCanvasGO = (GameObject)MonoBehaviour.Instantiate(Resources.Load("UI/Fade Canvas"));
            TransitionCanvas.fadePanel = FadeCanvasGO.transform.GetChild(0).GetComponent<Image>();
            TransitionCanvas.instance = FadeCanvasGO.GetComponent<TransitionCanvas>();
        }

        return TransitionCanvas.instance;
    }

    public void FadeIn(float fadeDuration)
    {
        this.StartCoroutine(FadeFunc.DoFadeInBlack((Color32 color) => TransitionCanvas.fadePanel.color = color, fadeDuration));
    }

    public void FadeOut(float fadeDuration)
    {
        this.StartCoroutine(FadeFunc.DoFadeOutBlack((Color32 color) => TransitionCanvas.fadePanel.color = color, fadeDuration));
    }
}