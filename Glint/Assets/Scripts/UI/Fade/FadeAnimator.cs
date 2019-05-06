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
            IFadeable[] fadeables = fadeableObject.GetComponents<IFadeable>();
            foreach (IFadeable fadeable in fadeables)
            {
                this.fadeables.Add(fadeable);
            }
        }
    }

    public void AllFadeIn()
    {
        foreach(IFadeable fadeable in this.fadeables)
        {
            StartCoroutine(fadeable.FadeAnimation());
        }
    }
}
