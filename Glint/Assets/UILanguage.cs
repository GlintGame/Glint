using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILanguage : MonoBehaviour {

    public static SystemLanguage language;

    void Awake()
    {
        UILanguage.language = Application.systemLanguage;
    }
}
