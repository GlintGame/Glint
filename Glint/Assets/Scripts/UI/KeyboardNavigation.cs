using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Luminosity.IO;

public class KeyboardNavigation : MonoBehaviour {

    //GameObject[] _inputButtons;

    void Awake () {
        //_inputButtons = GameObject.FindGameObjectsWithTag("inputButton");
    }
	

	void Update () {
		
	}


    void FixedUpdate()
    {

        /*foreach(GameObject button in _inputButtons)
        {
            if (InputManager.GetButton("UI submit")
                && EventSystem.current.currentSelectedGameObject == button)
            {
                Debug.Log("button is pressed");
            }
        }*/
    }
}
