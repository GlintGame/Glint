using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorFocusManager : MonoBehaviour
{
    EventSystem eventSystem;

    void Awake()
    {
        this.eventSystem = this.GetComponent<EventSystem>();
    }

    void OnGUI()
    {
        if(this.eventSystem.currentSelectedGameObject == null
            && SelectableScreen.activeScreens.Count != 0 )
        {
            SelectableScreen.activeScreens[SelectableScreen.activeScreens.Count - 1].Focus();
        }
    }
}
