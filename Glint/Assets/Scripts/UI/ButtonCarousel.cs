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

    public TMP_Text textMesh;
    public Button leftButton;
    public Button rightButton;

    public string[] TextArray;

    private string text
    {
        get { return this.textMesh.text; }
        set { this.textMesh.text = value; }
    }

    private int currentIndex = 0;
    public int CurrentIndex
    {
        get { return currentIndex; }
        set {
            if (value < 0)
                value = 0;

            if (value > this.TextArray.Length - 1)
                value = this.TextArray.Length - 1;

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
        this.text = this.TextArray[this.CurrentIndex];
    }

    private void UpdateButtons()
    {
        if (this.CurrentIndex == 0)
            this.leftButton.interactable = false;
        else
            this.leftButton.interactable = true;

        if (this.CurrentIndex == this.TextArray.Length - 1)
            this.rightButton.interactable = false;
        else
            this.rightButton.interactable = true;
    }
}
