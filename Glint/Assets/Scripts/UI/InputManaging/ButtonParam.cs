using UnityEngine;
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
        this.inputSetting = GameObject.Find("_InputSettings");
        this.keyBinder = this.inputSetting.GetComponentInChildren<KeyBinder>();
        this.nativeButtonScript = this.GetComponent<Button>();
        this.buttonText = this.GetComponentInChildren<Text>();
        this.UpdateButton();
    }

    public void KeyBind()
    {
        keyBinder.KeyBind(this.buttonParams, gameObject);
        nativeButtonScript.interactable = false;
        buttonText.text = "...";
    }

    public void UpdateButton()
    {
        int index = buttonParams.inputType == CustomInputType.KeyboardButton ? KeyBinder.keyboardBindingIndex : KeyBinder.joystickBindingIndex;
        inputAction = InputManager.GetAction(KeyBinder.controlScheme, buttonParams.action);
        binding = inputAction.Bindings[index];

        string outputText;

        /*
        switch (binding.Type)
        {
            case InputType.Button:
                outputText = buttonParams.isNegative ? binding.Negative.ToString() : binding.Positive.ToString();
                break;
            case InputType.AnalogButton:
                outputText = binding.Axis.ToString();
                break;
            default:
                outputText = "Error";
                break;
        }

        buttonText.text = outputText;
        */
        outputText = binding.Type.ToString();
    }

}
