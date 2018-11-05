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

    void OnGUI()
    {
        keyEvent = Event.current;
        if (keyEvent.type == EventType.KeyDown /*&& _waitingForKey*/)
        {
            //Debug.Log(keyEvent.keyCode);
            //Debug.Log(Translate(keyEvent.keyCode.ToString()));
            //_waitingForKey = false;
            //InputManagerManager.SetProperty(_axisName, _propertyName, keyEvent.keyCode);
        }
    }
}