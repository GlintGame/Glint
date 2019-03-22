using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Luminosity.IO;

public class CustomSelectableButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    private bool isFocused = false;
    private bool isSelected = false;

    private Selectable selectable;

    public GameObject eventSystemGameObject;
    private EventSystem eventSystem;

    public UnityEvent onPrevious;
    public UnityEvent onNext;
    public UnityEvent onSubmit;

    void Awake()
    {
        this.selectable = this.gameObject.GetComponent<Selectable>();
        this.eventSystem = this.eventSystemGameObject.GetComponent<EventSystem>();
    }

    void Update()
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
            this.Submit();
        }

        if(this.isSelected
            && (InputManager.GetAxis("UI_GPHorizontal") > 0
            || InputManager.GetButton("UI_Right")))
        {
            this.Next();
        }
        
        if (this.isSelected
            && (InputManager.GetAxis("UI_GPHorizontal") < 0
            || InputManager.GetButton("UI_Left")))
        {
            this.Previous();
        }
    }

    private void Select()
    {
        this.selectable.interactable = false;
        this.eventSystemGameObject.SetActive(false);
        this.isSelected = true;
    }

    private void Deselect()
    {
        this.selectable.interactable = true;
        this.eventSystemGameObject.SetActive(true);
        this.eventSystem.SetSelectedGameObject(this.gameObject);
        this.isSelected = false;
    }

    public virtual void Previous()
    {
        this.onPrevious.Invoke();
    }

    public virtual void Next()
    {
        this.onNext.Invoke();
    }

    public virtual void Submit()
    {
        this.onSubmit.Invoke();
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
