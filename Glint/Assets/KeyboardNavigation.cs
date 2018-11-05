using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Luminosity.IO;

public class KeyboardNavigation : MonoBehaviour {

    public GameObject btn;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        if (InputManager.GetButton("UI submit"))
        {
            Debug.Log("submit");
            if (EventSystem.current.currentSelectedGameObject == btn)
            {
                Debug.Log("button is pressed");
            }
        }
    }
}
