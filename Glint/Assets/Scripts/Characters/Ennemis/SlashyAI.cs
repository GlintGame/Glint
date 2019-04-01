using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashyAI : MonoBehaviour
{
    public float DieDuration = 1f;

    private SlashyStates state = SlashyStates.Idle;
    private enum SlashyStates
    {
        Follow,
        Hit,
        Idle,
    }

    private CharacterController2D CharacterController;
    private Collider2D Collider;
    private Animator Animator;
    private Rigidbody2D Rigidbody;
    private Transform Transform;
    private Transform target;
    
    // use incoming events to change state and hadle atacks
    public void OnDetected(GameObject target)
    {
        if (this.state == SlashyStates.Hit) return;

        this.state = SlashyStates.Follow;
        this.Animator.SetBool("walking", true);
        this.target = target.GetComponent<Transform>();
    }

    public void OnEscape()
    {
        this.state = SlashyStates.Idle;
        this.Animator.SetBool("walking", false);
        this.target = null;
    }

    public void OnReach()
    {
        if(this.state != SlashyStates.Hit) StartCoroutine(Slash());
    }

    public void OnDie()
    {
        this.Animator.SetTrigger("die");
        this.Rigidbody.bodyType = RigidbodyType2D.Static;
        this.Collider.enabled = false;
        PlayerScore.Kills++;
        StartCoroutine(utils.Coroutine.Do.Wait(this.DieDuration, () => Destroy(this.gameObject)));
    }

    private void Awake()
    {
        this.CharacterController = this.GetComponent<CharacterController2D>();
        this.Animator = this.GetComponent<Animator>();
        this.Transform = this.GetComponent<Transform>();
        this.Collider = this.GetComponent<Collider2D>();
        this.Rigidbody = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(this.state == SlashyStates.Follow && this.target != null)
        {
            float hmove = this.Transform.position.x < this.target.position.x ? 1 : -1;
            this.CharacterController.SetMove(hmove, true);
        }
        else
        {
            this.CharacterController.SetMove(0, true);
        }
    }

    public IEnumerator Slash()
    {
        this.state = SlashyStates.Hit;

        this.Animator.SetBool("walking", false);

        yield return new WaitForSeconds(0.5f);
        // attack !
        yield return new WaitForSeconds(0.5f);

        this.Animator.SetBool("walking", true);

        this.state = SlashyStates.Follow;
    }
}


