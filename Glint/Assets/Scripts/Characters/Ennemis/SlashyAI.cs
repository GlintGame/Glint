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

        AudioManager.Play("Slashy_detect");
        this.state = SlashyStates.Follow;
        this.Animator.SetBool("walking", true);
        this.target = target.GetComponent<Transform>();
    }

    public void OnEscape()
    {

        AudioManager.Stop("Slashy_detect");
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
        AudioManager.Play("Enemy_die");
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

    // slash related props
    public Vector2 SlashHitBoxOffset = new Vector2(3, 4);
    public Vector2 SlashHitboxSize = new Vector2(5, 6);
    public int SlashDamages = 15;

    public IEnumerator Slash()
    {

        AudioManager.Play("Slashy_hit");
        this.state = SlashyStates.Hit;

        this.Animator.SetTrigger("attack");

        yield return new WaitForSeconds(0.5f);

        Vector2 center = this.Transform.position;
        var offset = new Vector2(this.SlashHitBoxOffset.x * this.CharacterController.Direction, this.SlashHitBoxOffset.y);
        center += offset;

        Collider2D[] collided = Physics2D.OverlapAreaAll(center - this.SlashHitboxSize / 2, center + this.SlashHitboxSize / 2);
        Debug.DrawLine(center - this.SlashHitboxSize / 2, center + this.SlashHitboxSize / 2, Color.green);
        foreach (Collider2D collider in collided)
        {
            IHitable target = collider.gameObject.GetComponent<IHitable>();
            if (collider.gameObject != this.gameObject && target != null)
            {
                target.TakeDamages(this.SlashDamages, this.Transform.position);
            }
        }

        yield return new WaitForSeconds(0.5f);

        this.state = SlashyStates.Follow;
    }
}


