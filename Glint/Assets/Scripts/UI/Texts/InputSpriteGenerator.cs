using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils;

// Replace a group of char by sprites
public class InputSpriteGenerator : MonoBehaviour, ITextModifier
{

    public string keyboardString = "$/KB/";
    public BindingButton keyboardParams;

    public string gamepadString = "$/GP/";
    public BindingButton gamepadParams;

    public string ModifyText(string text)
    {
        if (this.keyboardParams)
            text = text.Replace(this.keyboardString, "<sprite=" + InputsDictionnary.getSpriteIndex(this.keyboardParams) + ">");

        if (this.gamepadParams)
            text = text.Replace(this.gamepadString, "<sprite=" + InputsDictionnary.getSpriteIndex(this.gamepadParams) + ">");

        return text;
    }

}
