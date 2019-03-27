using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils;

public class EggsyAI : MonoBehaviour
{
    private BoxCollider2D Collider;

    private void Awake()
    {
        this.Collider = this.GetComponent<BoxCollider2D>();
        this.Collider.enabled = false;
    }

    public void OnDetected()
    {
        this.Collider.enabled = true;

        StartCoroutine(utils.Coroutine.Do.Wait(3f, () => this.Collider.enabled = false));
    }

    public void OffDetected()
    {
        this.Collider.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // make dmg
    }
}
