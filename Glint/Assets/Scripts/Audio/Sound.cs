using System;
using UnityEngine;

[Serializable]
public class Sound : IEquatable<Sound> {

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

    // equality is based on name.
    public bool Equals(Sound other)
    {
        if (other == null) return false;
        return this.Name.Equals(other.Name);
    }

    public override int GetHashCode()
    {
        return this.Name.GetHashCode();
    }
}
