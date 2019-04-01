using UnityEngine;
using UnityEngine.Events;

public class PlayerPicker : MonoBehaviour, IPicker
{
    [System.Serializable]
    public class PlayerPickedEvent : UnityEvent<int> { }
    public PlayerPickedEvent onPickup;

    public void Pick(object picked)
    {
        if(picked is int)
        {
            PlayerScore.Luciole++;
        }
    }
}


