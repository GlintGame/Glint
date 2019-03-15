using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILanguage : MonoBehaviour {

    public string PlayerPrefLocation;
    public static SystemLanguage language;
    public TMP_Dropdown dropdown;

    void Awake()
    {
        if(PlayerPrefs.HasKey(this.PlayerPrefLocation))
        {
            UILanguage.language = (SystemLanguage)PlayerPrefs.GetInt(this.PlayerPrefLocation);
        }
        else
        {
            UILanguage.language = Application.systemLanguage;
            PlayerPrefs.SetInt(this.PlayerPrefLocation, (int)UILanguage.language);
        }

        this.dropdown.value = UILanguage.language == SystemLanguage.French ? 0 : 1;
    }

    public void ChangeLanguage(int value)
    {
        if(value == 0)
        {
            UILanguage.language = SystemLanguage.French;
        }
        else
        {
            UILanguage.language = SystemLanguage.English;
        }

        PlayerPrefs.SetInt(this.PlayerPrefLocation, (int)UILanguage.language);
    }

    public void UpdateTranslators()
    {
        TextManager.UpdateAllText();
    }
}
