using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenModeManager : MonoBehaviour
{
    public string PlayerPrefLocation;
    public ButtonCarousel ScreenModeButton;
    private static bool fullScreen;

    public static bool FullScreen
    {
        get { return fullScreen; }
        set
        {
            Screen.fullScreen = value;
            fullScreen = value;
        }
    }

    void Awake()
    {
        if (PlayerPrefs.HasKey(this.PlayerPrefLocation))
        {
            ScreenModeManager.fullScreen = PlayerPrefs.GetInt(this.PlayerPrefLocation) == 1;
        }
        else
        {
            ScreenModeManager.fullScreen = Screen.fullScreen;
            PlayerPrefs.SetInt(this.PlayerPrefLocation, ScreenModeManager.fullScreen ? 1 : 0);
        }

        this.ScreenModeButton.CurrentIndex = ScreenModeManager.fullScreen ? 0 : 1;
    }

    public void ChangeScreenMode(int value)
    {
        ScreenModeManager.fullScreen = value == 0;
        PlayerPrefs.SetInt(this.PlayerPrefLocation, ScreenModeManager.fullScreen ? 1 : 0);
    }
}
