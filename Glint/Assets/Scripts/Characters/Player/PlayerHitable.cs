using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using utils;

public class PlayerHitable : MonoBehaviour, IHitable, IRespawnable {

    private Vector3 respawnPotition;
    private Rigidbody2D Rigidbody;
    private Transform Transform;

    private void Awake()
    {
        this.Transform = this.GetComponent<Transform>();
        this.Rigidbody = this.GetComponent<Rigidbody2D>();
        this.respawnPotition = this.Transform.position;
    }

    public void respawn()
    {
        Debug.Log("respawning");
        this.StartCoroutine(utils.Coroutine.Do.StopLookAhead(1f));

        this.Rigidbody.velocity = Vector2.zero;
        this.Transform.position = this.respawnPotition;

    }

    public void setRespawn(Transform position)
    {
        this.respawnPotition = position.position;
    }

    public void TakeDamages(int damages, Vector3 origin)
    {
        this.respawn();
    }
    

}
