using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class SelectableScreen : MonoBehaviour
{
    public Selectable focusButton;
    public EventSystem eventSystem;
    public bool isActive;
    public abstract void Activate();
    public abstract void Desactivate();
    public abstract void Focus();
}
