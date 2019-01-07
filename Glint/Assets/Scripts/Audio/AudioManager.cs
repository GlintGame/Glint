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
            Debug.Log("pas d'instance de l'audio manager");
            AudioManager.instance = this;
        }
        else
        {
            Debug.Log("une instance de l'input manager, merging... ");
            AudioManager.instance.sounds = AudioManager.instance.sounds.Union(this.sounds).Distinct().ToArray();
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
        if (!sound.source.isPlaying)
        {
            sound.source.Play();
        }
    }

    public static void Stop(string name)
    {
        Sound sound = Array.Find(AudioManager.instance.sounds, s => s.Name == name);
        sound.source.Stop();
    }
}
