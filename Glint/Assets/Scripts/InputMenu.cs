using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils;
using System;

public class InputMenu : MonoBehaviour {

    Event keyEvent;
    private bool _waitingForKey = false;
    private InputManagerManager _inputManagerManager;
    private string _axisName;
    private string _propertyName;
    private Dictionary<string, string> _convertKeyToStr;
    private string _alphabetString = "abcdefghijklmnopqrstuvwxyz";
    private char[] _alphabetCharArray;

    void Awake()
    {
        this._inputManagerManager = gameObject.GetComponent<InputManagerManager>();

        this._alphabetCharArray = this._alphabetString.ToCharArray();
        
        this._convertKeyToStr = genDictionnary();
        Debug.Log("LeftShiftDdqa".CamelCaseTo_snake_case().SnakeToSpace());/*
        for (int i = 0; i < keyCode.Length; i++)
            keyCode.IndexOf("L");*/

        List<KeyCode> l = utils.Extentions.EnumToList<KeyCode>();
        foreach(KeyCode val in l)
        {
            Debug.Log(val);
        }
    }

    void OnGUI()
    {
        keyEvent = Event.current;
        if(keyEvent.isKey && _waitingForKey)
        {
            _waitingForKey = false;
            Debug.Log(keyEvent.keyCode);
            //InputManagerManager.SetProperty(_axisName, _propertyName, keyEvent.keyCode);
        }
    }

    private Dictionary<string, string> genDictionnary()
    {
        Dictionary<string, string> dictionnary = new Dictionary<string, string>();
        for(int i = 0; i < 26; i++)
        {
            dictionnary.Add(_alphabetCharArray[i].ToString(), _alphabetCharArray[i].ToString());
        }
        return dictionnary;
    }
}