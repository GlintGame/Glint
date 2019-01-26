﻿using UnityEngine;
using UnityEngine.UI;

using Luminosity.IO;

public class ButtonParam : MonoBehaviour {

    public BindingButton buttonParams;
    GameObject inputSetting;
    KeyBinder keyBinder;
    Button nativeButtonScript;
    Text buttonText;
    InputAction inputAction;
    InputBinding binding;

    void Awake()
    {
        this.inputSetting = GameObject.Find("InputSettings");
        this.keyBinder = this.inputSetting.GetComponentInChildren<KeyBinder>();
        this.nativeButtonScript = this.GetComponent<Button>();
    }

    void OnEnable()
    {
        this.buttonText = this.GetComponentInChildren<Text>();
    }

    public void KeyBind()
    {
        this.keyBinder.KeyBind(this.buttonParams, gameObject);
        nativeButtonScript.interactable = false;
        buttonText.text = "...";
    }

    public void UpdateButton()
    {

        int index = buttonParams.inputType == CustomInputType.KeyboardButton ? KeyBinder.keyboardBindingIndex : KeyBinder.gamepadBindingIndex;
        this.inputAction = InputManager.GetAction(KeyBinder.controlScheme, buttonParams.action);
        binding = inputAction.Bindings[index];
        
        string outputText;
        
        switch (buttonParams.inputType)
        {
            case CustomInputType.KeyboardButton:
            case CustomInputType.GamepadButton:
            case CustomInputType.DigitalAxis:
                outputText = buttonParams.isNegative ? binding.Negative.ToString() : binding.Positive.ToString();
                break;
            case CustomInputType.GamepadAxis:
                outputText = binding.Axis.ToString();
                break;
            default:
                outputText = "Error";
                break;
        }
        this.buttonText.text = outputText;
    }

}
