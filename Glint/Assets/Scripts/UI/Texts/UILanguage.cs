using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class UILanguage : MonoBehaviour {

    public string PlayerPrefLocation;
    public static SystemLanguage language;
    public ButtonCarousel LanguageButton;

    [Serializable]
    public struct LanguageAssoc
    {
        public string language;
        public SystemLanguage system;

        public LanguageAssoc(string p1, SystemLanguage p2)
        {
            this.language = p1;
            this.system = p2;
        }
    }

    public static LanguageAssoc[] Languages = {
        new LanguageAssoc("Français", SystemLanguage.French),
        new LanguageAssoc("English", SystemLanguage.English)
    };

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

        this.LanguageButton.CurrentIndex = UILanguage.language == SystemLanguage.French ? 0 : 1;
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

    public static int GetIndexFromSystemLanguage(SystemLanguage system)
    {
        for (int i = 0; i < UILanguage.Languages.Length; i++)
        {
            if (UILanguage.Languages[i].system == system)
                return i;
        }

        return 0;
    }
}
