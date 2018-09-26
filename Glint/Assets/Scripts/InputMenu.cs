using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMenu : MonoBehaviour {

    Event keyEvent;
    private bool _waitingForKey = false;
    private InputManagerManager _inputManagerManager;
    private string _axisName;
    private string _propertyName;

    void Awake()
    {
        this._inputManagerManager = gameObject.GetComponent<InputManagerManager>();
        genDictionnary();
    }

    void OnGUI()
    {
        Dictionary<KeyCode, string> convertKeyToStr;
        keyEvent = Event.current;
        if(keyEvent.isKey /*&& _waitingForKey*/)
        {
            _waitingForKey = false;
            Debug.Log(keyEvent.keyCode);
            //InputManagerManager.SetProperty(_axisName, _propertyName, keyEvent.keyCode);
        }
    }

    private Dictionary<string, string> genDictionnary()
    {
        Dictionary<string, string> dictionnary = new Dictionary<string, string>();
        string[] alphabet = new string[] {"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"};
        for(int i = 0; i < 26; i++)
        {
            dictionnary.Add(alphabet[i], alphabet[i]);
        }
        Debug.Log(dictionnary);
        return dictionnary;
    }
}