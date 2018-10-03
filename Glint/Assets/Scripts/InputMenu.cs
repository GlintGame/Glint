using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using utils;
using System;
using System.IO;

public class InputMenu : MonoBehaviour
{

    Event keyEvent;
    TextAsset dictionnary;
    Text buttonText;

    private InputManagerManager _inputManagerManager;
    private bool _waitingForKey = false;
    private string _axisName;
    private string _propertyName;

    void Awake()
    {
        this._inputManagerManager = gameObject.GetComponent<InputManagerManager>();
    }

    private string Translate(string v)
    {
        if(v.Contains("Arrow"))
        {
            v = v.Substring(0, v.Length - 5);
        }
        else if(v.Contains("Left") || v.Contains("Right"))
        {
            if (v.Contains("Apple"))
                v.Replace("Apple", "Cmd");
            v = v.CamelCaseTo_snake_case().SnakeToSpace();
        }
        v = v.CamelCaseTo_snake_case().SnakeToSpace();
        if (v.Contains("keypad"))
        {
            v = v.Substring(v.Length - 1);
            v = "[" + v + "]";
        }
        if (v.ToString().Contains("alpha"))
        {
            v = v.Substring(v.Length - 1);
        }
        return v.ToLower();
    }

    void OnGUI()
    {
        keyEvent = Event.current;
        if (keyEvent.type == EventType.KeyDown /*&& _waitingForKey*/)
        {
            Debug.Log(keyEvent.keyCode);
            Debug.Log(Translate(keyEvent.keyCode.ToString()));
            _waitingForKey = false;
            //InputManagerManager.SetProperty(_axisName, _propertyName, keyEvent.keyCode);
        }
    }
}