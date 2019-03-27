using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeCanvas
{
    private static Image fadePanel;

    public static void FadeIn()
    {
        FadeCanvas.getPanel();

        FadeCanvas.fadePanel.color = new Color32(255, 255, 225, 100);
    }

    public static void FadeOut()
    {
        FadeCanvas.getPanel();

        FadeCanvas.fadePanel.color = new Color32(255, 255, 225, 0);
    }

    public static void getPanel()
    {
        if(FadeCanvas.fadePanel == null)
        {
            GameObject FadeCanvasGO = (GameObject)MonoBehaviour.Instantiate(Resources.Load("UI/Fade Canvas"));
            FadeCanvas.fadePanel = FadeCanvasGO.transform.GetChild(0).GetComponent<Image>();
        }
    }
}
