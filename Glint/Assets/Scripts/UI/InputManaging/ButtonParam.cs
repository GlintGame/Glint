using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonParam : MonoBehaviour {

    public BindingButton buttonParams;
    GameObject inputSetting;
    KeyBinder keyBinder;
    Button nativeButtonScript;
    Text buttonText;

    void Awake()
    {
        this.inputSetting = GameObject.Find("_InputSettings");
        this.keyBinder = this.inputSetting.GetComponentInChildren<KeyBinder>();
        this.nativeButtonScript = this.GetComponent<Button>();
        this.buttonText = this.GetComponentInChildren<Text>();
    }

    public void KeyBind()
    {
        keyBinder.KeyBind(this.buttonParams, gameObject);
        nativeButtonScript.interactable = false;
        buttonText.text = "..."; 
    }
}
