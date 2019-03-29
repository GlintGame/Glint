using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class SelectableScreen : MonoBehaviour
{
    protected static List<SelectableScreen> activeScreens = new List<SelectableScreen>();
    public Selectable focusButton;
    public EventSystem eventSystem;
    public bool isActive;
    public abstract void Activate();
    public abstract void Desactivate();
    public abstract void Focus();

    protected void AddToActiveScreens(SelectableScreen instance)
    { SelectableScreen.activeScreens.Add(instance); }

    protected void RemoveFromActiveScreens(SelectableScreen instance)
    { SelectableScreen.activeScreens.Remove(instance); }

    protected bool AnotherIsActive(SelectableScreen instance)
    { return SelectableScreen.activeScreens.Count > 1; }
}
