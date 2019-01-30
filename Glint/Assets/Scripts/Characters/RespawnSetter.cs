using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSetter : MonoBehaviour {

    private Transform Transform;

    private void Awake()
    {
        this.Transform = this.GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IRespawnable respawnable = collision.gameObject.GetComponent<IRespawnable>();

        if (respawnable != null)
        {
            respawnable.setRespawn(this.Transform);
        }
    }
}
