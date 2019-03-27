using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils;

public class EggsyAI : MonoBehaviour
{
    public int Damages = 10;

    private BoxCollider2D Collider;
    private Transform Transform;

    private void Awake()
    {
        this.Collider = this.GetComponent<BoxCollider2D>();
        this.Transform = this.GetComponent<Transform>();
        this.Collider.enabled = false;
    }

    public void OnDetected()
    {
        this.Collider.enabled = true;

        StartCoroutine(
            utils.Coroutine.Do.Wait(3f, 
                () => this.Collider.enabled = false
            )
        );
    }

    public void OffDetected()
    {
        this.Collider.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var other = collision.gameObject.GetComponent<IHitable>();

        if (other != null && !(other is EnemyStats))
        {
            other.TakeDamages(this.Damages, this.Transform.position);
        }
    }
}
