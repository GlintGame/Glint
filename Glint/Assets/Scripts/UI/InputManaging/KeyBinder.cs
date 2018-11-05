using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

using Luminosity.IO;

public class KeyBinder : MonoBehaviour
{

    public int timeout = 10;
    public string controlScheme = "Player";
    public KeyCode cancelKey = KeyCode.Escape;
    public int keyboardBindingIndex = 0;
    public int joystickBindingIndex = 1;

    public void TranslateButtonParams(string buttonParams)
    {
        string[] tableParam = buttonParams.Split(' ');
        string inputType = tableParam[0];
        string action = tableParam[1];
        bool negative = tableParam[2] == "negative" ? true : false;

        KeyCode currentKey = (negative ? InputManager.GetAction(this.controlScheme, action).Bindings[inputType == "keyboard" ? 0 : 1].Negative : InputManager.GetAction(this.controlScheme, action).Bindings[inputType == "keyboard" ? 0 : 1].Positive);

        Debug.LogFormat("type : {0}, action : {1}, is negative : {2}, current key : {3}", inputType, action, negative, currentKey);

        KeyBind(inputType, action, negative);
    }

    void KeyBind(string type, string action, bool negative)
    {
        if (type != "keyboard"
            && type != "gamepad")
            Debug.LogError(string.Format("A binding type named \'{0}\' does not exist", type));

        
        ScanSettings settings = new ScanSettings
        {
            ScanFlags = type == "keyboard" ? ScanFlags.Key : ScanFlags.JoystickButton | ScanFlags.JoystickAxis,

            CancelScanKey = this.cancelKey,

            Timeout = this.timeout
        };

        if(type == "keyboard")
        {
            StartScanKeyboard(settings, action, negative);
        }
        else
        {
            StartScanGamepad(settings, action, negative);
        }        
    }


    void StartScanKeyboard(ScanSettings settings, string action, bool negative)
    {
        InputManager.StartInputScan(settings, result =>
        {
            int index = this.joystickBindingIndex;

            InputAction inputAction = InputManager.GetAction(this.controlScheme, action);
            inputBinding(inputAction, index, negative, result);
            
            Debug.Log(negative ? inputAction.Bindings[index].Negative : inputAction.Bindings[index].Positive);

            PlayerPrefsSave();
            return true;
        });
    }

    void StartScanGamepad(ScanSettings settings, string action, bool negative)
    {
        InputManager.StartInputScan(settings, result =>
        {
            int index = this.joystickBindingIndex;

            if (result.ScanFlags == ScanFlags.JoystickButton)
            {
                if ((int)result.Key < (int)KeyCode.JoystickButton0 || (int)result.Key > (int)KeyCode.JoystickButton19)
                    return false;

                InputAction inputAction = InputManager.GetAction(this.controlScheme, action);
                inputAction.Bindings[index].Type = InputType.Button;
                inputBinding(inputAction, index, negative, result);

                Debug.Log(negative ? inputAction.Bindings[index].Negative : inputAction.Bindings[index].Positive);
            }
            else
            {
                InputAction inputAction = InputManager.GetAction(this.controlScheme, action);
                inputAction.Bindings[index].Type = InputType.AnalogButton;
                inputAction.Bindings[index].Invert = negative ? !(result.JoystickAxisValue < 0.0f) : result.JoystickAxisValue < 0.0f;
                inputAction.Bindings[index].Axis = result.JoystickAxis;

                Debug.Log(inputAction.Bindings[index].Axis);
            }

            PlayerPrefsSave();
            return true;
        });
    }

    void inputBinding(InputAction inputAction, int index, bool negative, ScanResult result)
    {
        if (!negative)
        {
            inputAction.Bindings[index].Positive = result.Key;
        }
        else
        {
            inputAction.Bindings[index].Negative = result.Key;
        }
    }







    private void PlayerPrefsSave()
    {
        StringBuilder output = new StringBuilder();
        InputSaverXML saver = new InputSaverXML(output);
        InputManager.Save(saver);

        PlayerPrefs.SetString("MyGame.InputConfig", output.ToString());
    }

    private void PlayerPrefsLoad()
    {
        if (PlayerPrefs.HasKey("MyGame.InputConfig"))
        {
            string xml = PlayerPrefs.GetString("MyGame.InputConfig");
            using (TextReader reader = new StringReader(xml))
            {
                InputLoaderXML loader = new InputLoaderXML(reader);
                InputManager.Load(loader);
            }
        }
    }

}
