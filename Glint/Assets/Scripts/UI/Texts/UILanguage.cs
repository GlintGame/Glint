using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILanguage : MonoBehaviour {

    public static SystemLanguage language;

    void Awake()
    {
        UILanguage.language = Application.systemLanguage;
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
    }

    public void UpdateTranslators()
    {
        Translator.UpdateAll();
    }
}
