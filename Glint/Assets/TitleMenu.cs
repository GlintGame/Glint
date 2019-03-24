using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour
{
    public Button FocusButton;
    public EventSystem eventSystem;


    public void Focus()
    {
        this.eventSystem.SetSelectedGameObject(this.gameObject);
        this.FocusButton.Select();
    }
}
