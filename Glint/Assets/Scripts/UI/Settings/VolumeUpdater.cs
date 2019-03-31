using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class VolumeEvent : UnityEvent<float> {}

public class VolumeUpdater : MonoBehaviour {

    public VolumeEvent OnVolumeChange;

    private float minVolume;
    private float maxVolume;
    private float volume;
    public float Volume
    {
        get {
            return volume;
        }
        set {
            if(value <= this.minVolume)
                value = this.minVolume;
            if (value >= this.maxVolume)
                value = this.maxVolume;

            this.OnVolumeChange.Invoke(value);
            volume = value;
        }
    }
    private Slider slider;

    void Awake()
    {
        this.slider = this.gameObject.GetComponent<Slider>();
        this.minVolume = this.slider.minValue;
        this.maxVolume = this.slider.maxValue;
        this.volume = AudioManager.globalSoundMultiplier;
        this.UpdateSliderGUI();
        Debug.Log(AudioManager.globalSoundMultiplier);
    }

    public void UpdateSliderGUI()
    {
        this.slider.value = this.Volume;
    }

    public void IncreaseVolume(float amount)
    {
        this.Volume += amount;
        this.UpdateSliderGUI();
    }

    public void DecreaseVolume(float amount)
    {
        this.Volume -= amount;
        this.UpdateSliderGUI();
    }
}
