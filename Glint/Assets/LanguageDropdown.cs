using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanguageDropdown : MonoBehaviour {

    private TMP_Dropdown dropdown;

    void Awake()
    {
        this.dropdown = gameObject.GetComponent<TMP_Dropdown>();

        this.dropdown.value = 1;
    }
}
