using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Luminosity.IO;

public class GamepadSlider : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    private bool focused = false;
    private bool isSelected = false;
    //private Slider slider;
    private EventSystem eventSystem;
    public GameObject eventSystemGameObject;

    void Awake()
    {
        //this.slider = this.gameObject.GetComponent<Slider>();
        this.eventSystem = this.eventSystemGameObject.GetComponent<EventSystem>();
    }

    void Update () {
        if(this.focused
            && !this.isSelected
            && InputManager.GetButton("UI_Submit"))
        {
            Debug.Log(this.eventSystem);
            this.isSelected = true;
            this.eventSystemGameObject.SetActive(false);
        }

        if(this.isSelected
            && InputManager.GetButton("UI_Submit"))
        {
            this.isSelected = false;
            this.eventSystemGameObject.SetActive(true);
            this.eventSystem.SetSelectedGameObject(this.gameObject);
        }
	}

    public void OnSelect(BaseEventData baseEventData)
    {
        this.focused = true;
    }

    public void OnDeselect(BaseEventData baseEventData)
    {
        this.focused = false;
    }
}
