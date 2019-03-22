using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LanguageUpdater : MonoBehaviour
{
    private ButtonCarousel buttonCarousel;

    void Awake()
    {
        this.buttonCarousel = this.GetComponent<ButtonCarousel>();
        this.buttonCarousel.CurrentIndex = UILanguage.GetIndexFromSystemLanguage(UILanguage.language);
        this.buttonCarousel.TextArray = this.GetArrayFromUILanguages();
    }

    string[] GetArrayFromUILanguages()
    {
        string[] textOutput = new string[UILanguage.Languages.Length];

        for (int i = 0; i < UILanguage.Languages.Length; i++)
        {
            textOutput[i] = UILanguage.Languages[i].language;
        }

        return textOutput;
    }
}
