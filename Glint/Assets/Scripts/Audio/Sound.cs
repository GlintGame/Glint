using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound : IComparable {

    public AudioClip clip;
    public string Name
    {
        get
        {
            return this.clip.name;
        }
    }

    [Range(0f, 1f)]
    public float volume = 0.5f;
    public bool playDefault = false;
    public bool loop = true;

    [HideInInspector]
    public AudioSource source;
    public void Init(AudioSource source)
    {
        this.source = source;
        source.clip = this.clip;
        source.volume = this.volume;
        source.loop = this.loop;
    }

    public int CompareTo(object other)
    {
        if (other == null) return 1;

        Sound otherSound = other as Sound;
        if(otherSound == null)
        {
            throw new ArgumentException("object compared to not-a-sound");
        }
        else
        {
            return this.Name.CompareTo(otherSound.Name);
        }
    }
}
