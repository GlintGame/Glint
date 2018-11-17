using UnityEngine.Audio;
using System;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    public static AudioManager instance;
    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

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

    public static void Stop(string name)
    {
        Sound sound = Array.Find(AudioManager.instance.sounds, s => s.Name == name);
        sound.source.Stop();
    }
}
