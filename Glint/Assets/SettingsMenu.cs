using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public GameObject SettingsUI;
    public Button FocusButton;
    public EventSystem eventSystem;

    private bool settingsAreOpen = false;

    public delegate void OpenSettingsAction();
    public static event OpenSettingsAction OnOpenSettings;

    public delegate void CloseSettingsAction();
    public static event CloseSettingsAction OnCloseSettings;

    public void OpenSettings()
    {
        this.SettingsUI.SetActive(true);
        this.settingsAreOpen = true;

        this.eventSystem.SetSelectedGameObject(this.SettingsUI);
        this.FocusButton.Select();

        if (OnOpenSettings != null)
            OnOpenSettings();
    }

    public void CloseSettings()
    {
        this.SettingsUI.SetActive(false);
        this.settingsAreOpen = false;

        if (OnCloseSettings != null)
            OnCloseSettings();
    }
}
