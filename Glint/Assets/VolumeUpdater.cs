using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class VolumeEvent : UnityEvent<float> {}

public class VolumeUpdater : MonoBehaviour {

    public VolumeEvent OnVolumeChange;

    private float volume;
    public float Volume
    {
        get {
            return volume;
        }
        set {
            if(volume != value)
            {
                PlayerPrefs.SetFloat(this.PlayerPrefsLocation, value);
            }
            this.OnVolumeChange.Invoke(value);
            volume = value;
        }
    }

    public string PlayerPrefsLocation;
    private Slider slider;

    void Awake()
    {
        this.slider = this.gameObject.GetComponent<Slider>();

        if(PlayerPrefs.HasKey(this.PlayerPrefsLocation))
        {
            this.Volume = PlayerPrefs.GetFloat(this.PlayerPrefsLocation);
            this.slider.value = this.Volume;
        }
    }
}
