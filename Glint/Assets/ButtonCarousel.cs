using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class ButtonCarouselEvent : UnityEvent<int> { }

public class ButtonCarousel : MonoBehaviour
{
    public TMP_Text textMesh;
    public Button leftButton;
    public Button rightButton;
    public string[] TextArray;
    public ButtonCarouselEvent onChange;

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

    public void PreviousLanguage()
    {
        this.CurrentIndex -= 1;
        this.onChange.Invoke(this.CurrentIndex);
    }

    public void NextLanguage()
    {
        this.CurrentIndex += 1;
        this.onChange.Invoke(this.CurrentIndex);
    }

    public void UpdateText()
    {
        this.text = this.TextArray[this.CurrentIndex];
    }

    public void UpdateButtons()
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
