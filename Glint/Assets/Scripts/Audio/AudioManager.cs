using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    public static AudioManager instance;
    
    public static float globalSoundMultiplier = 1;
    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneUnloaded += this.OnNewScene;

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
        if (sound != null && !sound.source.isPlaying)
        {
            sound.volume *= AudioManager.globalSoundMultiplier;
            sound.source.Play();
        }
    }

    public static void Stop(string name)
    {
        Sound sound = Array.Find(AudioManager.instance.sounds, s => s.Name == name);
        if(sound != null)
        {
            sound.source.Stop();
        }
    }

    public void ChangeVolume(float vol)
    {
        AudioManager.globalSoundMultiplier = vol;
        foreach(Sound sound in this.sounds)
        {
            sound.UpdateVolume(vol);
        }
    }
    
    public void OnNewScene(Scene aScene)
    {
        foreach (Sound sound in this.sounds)
        {
            if (sound.onlyOnSetScene)
            {
                AudioManager.Stop(sound.Name);
            }
        }
    }
}
