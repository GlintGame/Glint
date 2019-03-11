using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Luminosity.IO;

public class GamepadSlider : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    private bool isFocused = false;
    private bool isSelected = false;

    private Slider slider;

    private VolumeUpdater volumeUpdater;

    public GameObject eventSystemGameObject;
    private EventSystem eventSystem;

    void Awake()
    {
        this.volumeUpdater = this.gameObject.GetComponent<VolumeUpdater>();
        this.slider = this.gameObject.GetComponent<Slider>();
        this.eventSystem = this.eventSystemGameObject.GetComponent<EventSystem>();
    }

    void Update ()
    {
        if (this.isFocused
            && !this.isSelected
            && InputManager.GetButtonDown("UI_Submit"))
        {
            this.Select();
        }
        else if (this.isSelected
                && (InputManager.GetButtonDown("UI_Submit")
                || InputManager.GetButtonDown("UI_Cancel")))
        {
            this.Deselect();
        }

        if(this.isSelected
            && (InputManager.GetAxis("UI_GPHorizontal") > 0
            || InputManager.GetButton("UI_Right")))
        {
            Debug.Log(this.volumeUpdater.Volume);
            this.volumeUpdater.Volume = this.volumeUpdater.Volume + 0.01f;
            this.volumeUpdater.UpdateSliderGUI();
        }
        
        if (this.isSelected
            && (InputManager.GetAxis("UI_GPHorizontal") < 0
            || InputManager.GetButton("UI_Left")))
        {
            Debug.Log(this.volumeUpdater.Volume);
            this.volumeUpdater.Volume = this.volumeUpdater.Volume - 0.01f;
            this.volumeUpdater.UpdateSliderGUI();
        }
    }

    private void Select()
    {
        this.slider.interactable = false;
        this.eventSystemGameObject.SetActive(false);
        this.isSelected = true;
    }

    private void Deselect()
    {
        this.slider.interactable = true;
        this.eventSystemGameObject.SetActive(true);
        this.eventSystem.SetSelectedGameObject(this.gameObject);
        this.isSelected = false;
    }

    public void OnSelect(BaseEventData baseEventData)
    {
        this.isFocused = true;
    }

    public void OnDeselect(BaseEventData baseEventData)
    {
        this.isFocused = false;
    }
}
