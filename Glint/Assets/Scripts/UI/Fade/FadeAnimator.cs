using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using utils;

public class FadeAnimator : MonoBehaviour
{
    private List<IFadeable> fadeables = new List<IFadeable> {};
    public GameObject[] fadeableObjects = new GameObject[] {};

    void Awake()
    {
        foreach(GameObject fadeableObject in this.fadeableObjects)
        {
            IFadeable fadeable = fadeableObject.GetComponent<IFadeable>();
            if (fadeable != null)
                this.fadeables.Add(fadeable);
        }
    }

    public void AllFadeIn()
    {
        foreach(IFadeable fadeable in this.fadeables)
        {
            fadeable.Opacity = 0f;
            StartCoroutine(this.OneFadeIn(fadeable));
        }
    }

    public IEnumerator OneFadeIn(IFadeable fadeable)
    {
        yield return StartCoroutine(utils.Coroutine.WaitForRealSeconds(fadeable.StartTime));
        StartCoroutine(FadeFunc.DoFadeOpacity((opacity) => fadeable.Opacity = opacity, 0f, 1f, fadeable.Duration));
    }
}
