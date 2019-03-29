using UnityEngine;

public class SettingsMenu : SelectableScreen
{
    public GameObject SettingsUI;
    
    public delegate void OpenSettingsAction();
    public static event OpenSettingsAction OnOpenSettings;

    public delegate void CloseSettingsAction();
    public static event CloseSettingsAction OnCloseSettings;

    public override void Activate()
    {
        base.AddToActiveScreens(this);

        this.SettingsUI.SetActive(true);
        this.isActive = true;

        this.Focus();

        if (OnOpenSettings != null)
            OnOpenSettings();
    }

    public override void Desactivate()
    {
        base.RemoveFromActiveScreens(this);

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
