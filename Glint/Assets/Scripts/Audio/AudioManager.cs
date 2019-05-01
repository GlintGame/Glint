using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public string PlayerPrefsLocation = "Glint.Volume";

    public List<Sound> sounds;
    private List<Sound> registeredSounds;

    public static AudioManager instance;
    public static float globalSoundMultiplier = 0.9f;

    private void Start()
    {
        SceneManager.sceneUnloaded += this.OnNewScene;

        // singleton logic
        if (instance == null)
        {
            instance = this;
            instance.registeredSounds = new List<Sound>();

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        // update volume
        if (PlayerPrefs.HasKey(this.PlayerPrefsLocation))
        {
            //this.ChangeVolume(PlayerPrefs.GetFloat(this.PlayerPrefsLocation));
            UpdateVolume(PlayerPrefs.GetFloat(this.PlayerPrefsLocation));
        }

        // register only new sounds
        foreach (Sound sound in this.sounds)
        {
            var alreadyExistingSound = instance.registeredSounds.Find(s => s.Equals(sound));

            if (alreadyExistingSound != null)
            {
                if(!alreadyExistingSound.source.isPlaying)
                {
                    Play(alreadyExistingSound.Name);
                }
            }
            else
            {
                instance.registeredSounds.Add(sound);
                sound.Init(instance.gameObject.AddComponent<AudioSource>());
            }
        }
    }

    public static void Play(string name)
    {
        Sound sound = instance.registeredSounds.Find(s => s.Name == name);

        if (sound != null && !sound.source.isPlaying)
        {
            sound.source.Play();
        }
    }

    public void PlayFromInstance(string name)
    {
        Play(name);
    }

    public static void Stop(string name)
    {
        Sound sound = instance.registeredSounds.Find(s => s.Name == name);

        if (sound != null)
        {
            sound.source.Stop();
        }
    }

    //public void ChangeVolume(float vol)
    //{
    //    globalSoundMultiplier = vol;
    //    foreach (Sound sound in instance.registeredSounds)
    //    {
    //        sound.UpdateVolume(vol);
    //    }

    //    PlayerPrefs.SetFloat(this.PlayerPrefsLocation, vol);
    //}

    public static void UpdateVolume(float vol)
    {
        globalSoundMultiplier = vol;
        foreach (Sound sound in instance.registeredSounds)
        {
            sound.UpdateVolume(vol);
        }

        PlayerPrefs.SetFloat(instance.PlayerPrefsLocation, vol);
    }

    public void OnNewScene(Scene aScene)
    {
        foreach (Sound sound in this.sounds)
        {
            if (sound.onlyOnSetScene)
            {
                Stop(sound.Name);
            }
        }
    }
}
