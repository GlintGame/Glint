using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class ButtonCarouselEvent : UnityEvent<int> { }

public class ButtonCarousel : CustomSelectableButton
{
    public ButtonCarouselEvent onChange;

    public Button leftButton;
    public Button rightButton;

    public GameObject[] OptionArray;

    private int currentIndex = 0;
    public int CurrentIndex
    {
        get { return currentIndex; }
        set {
            if (value < 0)
                value = 0;

            if (value > this.OptionArray.Length - 1)
                value = this.OptionArray.Length - 1;

            currentIndex = value;
            this.UpdateText();
            this.UpdateButtons();
        }
    }

    void Start()
    {
        this.UpdateText();
    }


    public override void Previous()
    {
        this.CurrentIndex -= 1;
        this.onChange.Invoke(this.CurrentIndex);
        this.onPrevious.Invoke();
    }

    public override void Next()
    {
        this.CurrentIndex += 1;
        this.onChange.Invoke(this.CurrentIndex);
        this.onNext.Invoke();
    }

    private void UpdateText()
    {
        foreach(GameObject TMPOption in this.OptionArray)
        {
            TMPOption.SetActive(false);
        }

        this.OptionArray[this.CurrentIndex].SetActive(true);
    }

    private void UpdateButtons()
    {
        if (this.CurrentIndex == 0)
            this.leftButton.interactable = false;
        else
            this.leftButton.interactable = true;

        if (this.CurrentIndex == this.OptionArray.Length - 1)
            this.rightButton.interactable = false;
        else
            this.rightButton.interactable = true;
    }
}
