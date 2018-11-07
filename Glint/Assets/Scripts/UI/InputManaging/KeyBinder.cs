using UnityEngine;
using UnityEngine.UI;

using Luminosity.IO;

public class KeyBinder : MonoBehaviour
{
    public static int timeout = 10;
    public static string controlScheme = "Player";
    public static KeyCode cancelKey = KeyCode.Escape;
    public static int keyboardBindingIndex = 0;
    public static int joystickBindingIndex = 1;

    GameObject[] buttons;
    GameObject eventSystem;
    Event keyEvent;

    void Awake()
    {
        InputLoader.PlayerPrefsLoad();
        this.buttons = GameObject.FindGameObjectsWithTag("InputButton");
        this.eventSystem = GameObject.Find("EventSystem");

        foreach (GameObject button in buttons)
        {
            UpdateButton(button);
        }
    }

    private void UpdateButton(GameObject button)
    {
        BindingButton buttonParams = button.GetComponent<ButtonParam>().buttonParams;
        int index = buttonParams.inputType == CustomInputType.KeyboardButton ? KeyBinder.keyboardBindingIndex : KeyBinder.joystickBindingIndex;
        Text textComponent = button.GetComponentInChildren<Text>();
        InputAction inputAction = InputManager.GetAction(KeyBinder.controlScheme, buttonParams.action);
        InputBinding binding = inputAction.Bindings[index];

        string outputText;

        switch(binding.Type)
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

        textComponent.text = outputText;
    }


    public void KeyBind(BindingButton buttonParams, GameObject button)
    {
        this.eventSystem.SetActive(false);
        ScanSettings scanSettings = new ScanSettings
        {
            ScanFlags = buttonParams.inputType == CustomInputType.KeyboardButton ? ScanFlags.Key : ScanFlags.JoystickButton | ScanFlags.JoystickAxis,

            CancelScanKey = cancelKey,

            Timeout = timeout
        };

        if(buttonParams.inputType == CustomInputType.KeyboardButton)
        {
            StartScanKeyboard(scanSettings, buttonParams, button);
        }
        else
        {
            StartScanGamepad(scanSettings, buttonParams, button);
        }        
    }


    void StartScanKeyboard(ScanSettings scanSettings, BindingButton buttonParams, GameObject button)
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

    void StartScanGamepad(ScanSettings scanSettings, BindingButton buttonParams, GameObject button)
    {
        InputManager.StartInputScan(scanSettings, result =>
        {
            int index = KeyBinder.joystickBindingIndex;

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
        Button buttonScript = button.GetComponent<Button>();
        Debug.Log(button.name);
        buttonScript.interactable = true;
        buttonScript.Select();

        UpdateButton(button);

        this.eventSystem.SetActive(true);
        InputLoader.PlayerPrefsSave();
    }






}
