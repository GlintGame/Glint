using UnityEngine.Audio;
using System;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    public static AudioManager instance;

    [Range(0f, 1.5f)]
    public static float globalSoundMultiplier = 1;
    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if(AudioManager.instance == null)
        {
            AudioManager.instance = this;
        }
        else
        {
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
            sound.volume *= AudioManager.globalSoundMultiplier;
            sound.source.Play();
        }
    }

    public static void Stop(string name)
    {
        Sound sound = Array.Find(AudioManager.instance.sounds, s => s.Name == name);
        sound.source.Stop();
    }

    public void ChangeVolume(float vol)
    {
        // vol varie de 0 à 1 et la fonction est appellée à chaque fois que la valeur du slider est changée
        Debug.Log(vol);
    }
}
