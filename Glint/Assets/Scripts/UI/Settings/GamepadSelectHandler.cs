using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Luminosity.IO;

public class GamepadSelectHandler : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    private bool isFocused = false;
    private bool isSelected = false;

    private Selectable slider;

    public GameObject eventSystemGameObject;
    private EventSystem eventSystem;

    public UnityEvent onLeft;
    public UnityEvent onRight;
    public UnityEvent onSubmit;

    void Awake()
    {
        this.slider = this.gameObject.GetComponent<Selectable>();
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
            this.onSubmit.Invoke();
        }

        if(this.isSelected
            && (InputManager.GetAxis("UI_GPHorizontal") > 0
            || InputManager.GetButton("UI_Right")))
        {
            this.onRight.Invoke();
        }
        
        if (this.isSelected
            && (InputManager.GetAxis("UI_GPHorizontal") < 0
            || InputManager.GetButton("UI_Left")))
        {
            this.onLeft.Invoke();
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
