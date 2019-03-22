using UnityEngine;
using UnityEngine.Events;

public class PlayerPicker : MonoBehaviour, IPicker
{
    [System.Serializable]
    public class PlayerPickedEvent : UnityEvent<int> { }
    public PlayerPickedEvent onPickup;

    private int pickedNumber = 0;

    public void Pick(object picked)
    {
        if(picked is int)
        {
            this.pickedNumber += (int)picked;
        }

        this.onPickup.Invoke(this.pickedNumber);
    }
}


