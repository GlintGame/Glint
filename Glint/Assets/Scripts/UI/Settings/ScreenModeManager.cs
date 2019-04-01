using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenModeManager : MonoBehaviour
{
    public string PlayerPrefLocation;
    public ButtonCarousel ScreenModeButton;

    void Awake()
    {
        bool isFullscreen = Screen.fullScreen;
        if (PlayerPrefs.HasKey(this.PlayerPrefLocation))
        {
            isFullscreen = PlayerPrefs.GetInt(this.PlayerPrefLocation) == 1;
            Screen.fullScreen = isFullscreen;
        }
        else
        {
            PlayerPrefs.SetInt(this.PlayerPrefLocation, isFullscreen ? 1 : 0);
        }

        this.ScreenModeButton.CurrentIndex = isFullscreen ? 0 : 1;
    }

    public void ChangeScreenMode(int value)
    {
        bool isFullscreen = value == 0;
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt(this.PlayerPrefLocation, isFullscreen ? 1 : 0);
    }
}
