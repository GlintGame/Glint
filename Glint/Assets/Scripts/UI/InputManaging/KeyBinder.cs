﻿using UnityEngine;
using UnityEngine.UI;

using Luminosity.IO;

public class KeyBinder : MonoBehaviour
{
    public static int timeout = 10;
    public static string controlScheme = "Player";
    public static KeyCode cancelKey = KeyCode.Escape;
    public static int keyboardBindingIndex = 0;
    public static int gamepadBindingIndex = 1;
    public string standardInputSave = "Glint.InputConfig";
    public string defaultInputSave = "Glint.DefaultInputConfig";

    GameObject[] buttons;
    GameObject eventSystem;
    Event keyEvent;

    void Awake()
    {
        this.buttons = GameObject.FindGameObjectsWithTag("InputButton");
        this.eventSystem = GameObject.Find("EventSystem");
        
        InputLoader.PlayerPrefsLoad(standardInputSave);
        this.UpdateAllButtons();
    }

    public void ResetBind()
    {
        InputLoader.PlayerPrefsDelete(standardInputSave);
        InputLoader.PlayerPrefsLoad(defaultInputSave);
        this.UpdateAllButtons();
    }

    void UpdateAllButtons()
    {
        foreach (GameObject button in this.buttons)
        {
            ButtonParam buttonParam = button.GetComponent<ButtonParam>();
            buttonParam.UpdateButton();
        }
    }

    public void KeyBind(BindingButton buttonParams, GameObject button)
    {
        this.eventSystem.SetActive(false);

        ScanFlags scanFlagOut = ScanFlags.Key;
        switch (buttonParams.inputType)
        {
            case CustomInputType.KeyboardButton:
                scanFlagOut = ScanFlags.Key;
                break;
            case CustomInputType.GamepadButton:
                scanFlagOut = ScanFlags.JoystickButton | ScanFlags.JoystickAxis;
                break;
            case CustomInputType.GamepadAxis:
                scanFlagOut = ScanFlags.JoystickAxis;
                break;
            default:
                Debug.LogErrorFormat("{1} Not implemented yet", buttonParams.inputType);
                break;
        }

        ScanSettings scanSettings = new ScanSettings
        {
            ScanFlags = scanFlagOut,
            CancelScanKey = cancelKey,
            Timeout = timeout
        };

        switch (buttonParams.inputType)
        {
            case CustomInputType.KeyboardButton:
            case CustomInputType.DigitalAxis:
                StartScanKBButton(scanSettings, buttonParams, button);
                break;
            case CustomInputType.GamepadButton:
                StartScanGPButton(scanSettings, buttonParams, button);
                break;
            case CustomInputType.GamepadAxis:
                StartScanGPAxis(scanSettings, buttonParams, button);
                break;
            default:
                Debug.LogErrorFormat("{1} Not implemented yet", buttonParams.inputType);
                break;
        }
    }


    void StartScanKBButton(ScanSettings scanSettings, BindingButton buttonParams, GameObject button)
    {
        InputManager.StartInputScan(scanSettings, result =>
        {
            int index = KeyBinder.keyboardBindingIndex;

            InputAction inputAction = InputManager.GetAction(KeyBinder.controlScheme, buttonParams.action);
            InputAttribution(inputAction, index, buttonParams.isNegative, result);

            EndScan(button);
            return true;
        });
    }

    void StartScanGPButton(ScanSettings scanSettings, BindingButton buttonParams, GameObject button)
    {
        InputManager.StartInputScan(scanSettings, result =>
        {
            int index = KeyBinder.gamepadBindingIndex;

            if (result.ScanFlags == ScanFlags.JoystickButton)
            {
                if ((int)result.Key < (int)KeyCode.JoystickButton0 || (int)result.Key > (int)KeyCode.JoystickButton19)
                    return false;

                InputAction inputAction = InputManager.GetAction(KeyBinder.controlScheme, buttonParams.action);
                inputAction.Bindings[index].Type = InputType.Button;
                InputAttribution(inputAction, index, buttonParams.isNegative, result);
            }
            else
            {
                InputAction inputAction = InputManager.GetAction(KeyBinder.controlScheme, buttonParams.action);
                inputAction.Bindings[index].Type = InputType.AnalogButton;
                inputAction.Bindings[index].Invert = buttonParams.isNegative ? !(result.JoystickAxisValue < 0.0f) : result.JoystickAxisValue < 0.0f;
                inputAction.Bindings[index].Axis = result.JoystickAxis;
            }

            EndScan(button);
            return true;
        });
    }

    void StartScanGPAxis(ScanSettings settings, BindingButton buttonParams, GameObject button)
    {
        int index = KeyBinder.gamepadBindingIndex;

        InputManager.StartInputScan(settings, result => {
            InputAction inputAction = InputManager.GetAction(KeyBinder.controlScheme, buttonParams.action);
            inputAction.Bindings[index].Axis = result.JoystickAxis;

            EndScan(button);
            return true;
        });
    }

    void InputAttribution(InputAction inputAction, int index, bool negative, ScanResult result)
    {
        if (negative)
        {
            inputAction.Bindings[index].Negative = result.Key;
        }
        else
        {
            inputAction.Bindings[index].Positive = result.Key;
        }
    }

    void EndScan(GameObject button)
    {
        Debug.Log("Scan End");
        Button buttonScript = button.GetComponent<Button>();
        buttonScript.interactable = true;
        buttonScript.Select();

        ButtonParam buttonParam = button.GetComponent<ButtonParam>();
        buttonParam.UpdateButton();

        this.eventSystem.SetActive(true);
        InputLoader.PlayerPrefsSave(standardInputSave);
    }
}
