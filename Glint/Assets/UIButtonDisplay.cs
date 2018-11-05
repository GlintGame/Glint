using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Luminosity.IO;

public class UIButtonDisplay : MonoBehaviour {
    
    public string controlScheme = "Player";
    public string action = "";
    public bool negative = false;
    public string type = "keyboard";
    public int keyboardBindingIndex = 0;
    public int joystickBindingIndex = 1;
    Text _textComponent;
    InputAction _inputAction;
    InputBinding _binding;


    // Use this for initialization
    void Start () {
        if (type != "keyboard"
            && type != "gamepad")
            Debug.LogError(string.Format("A binding type named \'{0}\' does not exist", type));

        int index = type == "keyboard" ? this.keyboardBindingIndex : this.joystickBindingIndex;
        string outputText = "unknown type";

        this._textComponent = GetComponentInChildren<Text>();
        this._inputAction = InputManager.GetAction(this.controlScheme, action);
        this._binding = this._inputAction.Bindings[index];
        
        Debug.Log(this._binding.Type);

        if (this._binding.Type == InputType.Button)
        {
            outputText = this.negative ? this._binding.Negative.ToString() : this._binding.Positive.ToString();
        }
        else if (this._binding.Type == InputType.AnalogButton)
        {
            outputText = this._binding.Axis.ToString();
        }
        this._textComponent.text = outputText;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
