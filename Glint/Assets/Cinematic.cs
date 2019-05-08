using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Cinematic : MonoBehaviour
{

    private VideoPlayer videoPlayer;
    public string nextScene;
    public UISceneManager sceneManager;
    public FadeAnimator fadeAnimator;

    void Awake()
    {
        this.videoPlayer = gameObject.GetComponent<VideoPlayer>();
        this.videoPlayer.loopPointReached += (UnityEngine.Video.VideoPlayer vp) => this.sceneManager.LoadScene(nextScene);
    }

    void Start()
    {
        this.fadeAnimator.AllFadeIn();
        this.videoPlayer.Play();
        AudioManager.Play("Cinématique Audio");
    }
}
