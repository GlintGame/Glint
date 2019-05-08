using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils;

public class EggsyAI : MonoBehaviour
{
    public int Damages = 10;
    public float DieDuration = 1f;

    enum EggsyStates
    {
        Run,
        Idle
    }

    private EggsyStates state = EggsyStates.Idle;

    private BoxCollider2D Collider;
    private Transform Transform;
    private Animator Animator;
    private CharacterController2D CharacterController;
    private Rigidbody2D Rigidbody;

    private Transform target;

    private void Awake()
    {
        this.Collider = this.GetComponent<BoxCollider2D>();
        this.Transform = this.GetComponent<Transform>();
        this.Animator = this.GetComponent<Animator>();
        this.CharacterController = this.GetComponent<CharacterController2D>();
        this.Rigidbody = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(this.state == EggsyStates.Run && this.target != null)
        {
            float hmove = this.Transform.position.x < this.target.position.x ? 1 : -1;
            this.CharacterController.SetMove(-hmove, true);
        }
        else
        {
            this.CharacterController.SetMove(0, true);
        }
    }

    public void OnDetected(GameObject target)
    {
        this.target = target.GetComponent<Transform>();
        this.state = EggsyStates.Run;
        AudioManager.Play("Eggsy_flee");
        this.Animator.SetBool("has seen player", true);
    }

    public void OffDetected()
    {
        this.target = null;
        this.state = EggsyStates.Idle;
        this.Animator.SetBool("has seen player", false);
    }

    public void OnDie()
    {
        this.Animator.SetTrigger("die");
        this.Rigidbody.bodyType = RigidbodyType2D.Static;
        this.Collider.enabled = false;
        AudioManager.Play("Enemy_die");
        PlayerScore.Kills++;
        StartCoroutine( utils.Coroutine.Do.Wait(this.DieDuration, () => Destroy(this.gameObject) ) );
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
