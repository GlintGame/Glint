using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using utils;

public class InputSpriteGenerator : MonoBehaviour {

    public string keyboardString = "$/KB/";
    public BindingButton keyboardParams;

    public string gamepadString = "$/GP/";
    public BindingButton gamepadParams;

    public string ReplaceToSprite(string text)
    {
        if (this.keyboardParams)
            text = text.Replace(this.keyboardString, "<sprite=" + InputsDictionnary.getSpriteIndex(this.keyboardParams) + ">");

        if (this.gamepadParams)
            text = text.Replace(this.gamepadString, "<sprite=" + InputsDictionnary.getSpriteIndex(this.gamepadParams) + ">");

        return text;
    }

}
