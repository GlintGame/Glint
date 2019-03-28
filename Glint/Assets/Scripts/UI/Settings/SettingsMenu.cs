using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsMenu : SelectableScreen
{
    public GameObject SettingsUI;
    
    public delegate void OpenSettingsAction();
    public static event OpenSettingsAction OnOpenSettings;

    public delegate void CloseSettingsAction();
    public static event CloseSettingsAction OnCloseSettings;

    public override void Activate()
    {
        this.SettingsUI.SetActive(true);
        this.isActive = true;

        this.Focus();

        if (OnOpenSettings != null)
            OnOpenSettings();
    }

    public override void Desactivate()
    {
        this.SettingsUI.SetActive(false);
        this.isActive = false;

        if (OnCloseSettings != null)
            OnCloseSettings();
    }

    public override void Focus()
    {
        this.eventSystem.SetSelectedGameObject(this.SettingsUI);
        this.focusButton.Select();
    }
}
