using UnityEngine.Audio;
using System;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    public static AudioManager instance;

    // TODO: add dont destroy on load and stopping sounds
    private void Awake()
    {
        if(AudioManager.instance == null)
        {
            AudioManager.instance = this;
        }
        else
        {
            AudioManager.instance.sounds = this.sounds.Union(AudioManager.instance.sounds).ToArray();
        }
        
        foreach(Sound sound in this.sounds)
        {
            sound.Init(this.gameObject.AddComponent<AudioSource>());
            if (sound.playDefault)
            {
                AudioManager.Play(sound.Name);
            }
        }
    }

    public static void Play(string name)
    {
        Sound sound = Array.Find(AudioManager.instance.sounds, s => s.Name == name);
        sound.source.Play();
    }

    // TODO: perhaps optimise this thing a bit
    public static void Play(string[] names)
    {
        foreach(string name in names)
        {
            AudioManager.Play(name);
        }
    }
}
