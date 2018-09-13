using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
    [Range(.2f, 1f)] public float WalkSpeedModifier = .8f;
    [Range(.2f, 1f)] public float InAirSpeedModifier = .6f;
    [Range(0, .3f)] public float MovementSmothing = .05f;
    [Range(0, 60f)] public float JumpForce = 20f;
    public Rigidbody2D RigidBody;
    public LayerMask GroundLayer;

    public UnityEvent OnLanding;
    
    private Vector3 _velocity = Vector3.zero;
    private bool _grounded = true;
    private bool _facingRight = true;
    private const float GroundedRadius = 1f;
    private ContactPoint2D[] _colisionContacts;

    public void Move(float xspeed, bool jump, bool running)
    {

        // set the horizontal speed
        if (!this._grounded)
        {
            xspeed *= this.InAirSpeedModifier;
        }
        else if(!running)
        {
            xspeed *= this.WalkSpeedModifier;
        }

        if (xspeed < 0 && _facingRight)
        {
            this.FlipSprite();
        }
        else if (xspeed > 0 && !_facingRight)
        {
            this.FlipSprite();
        }

        Vector3 targetVelocity = new Vector2(xspeed, this.RigidBody.velocity.y);
        this.RigidBody.velocity = Vector3.SmoothDamp(this.RigidBody.velocity, targetVelocity, ref this._velocity, this.MovementSmothing);

        // jump mechanic
        if (jump && this._grounded)
        {
            var jumpVector = new Vector2(this.RigidBody.velocity.x , this.JumpForce);
            this.RigidBody.velocity = jumpVector;

            this._grounded = false;
        }
    }

    private void Awake()
    {
        this._colisionContacts = new ContactPoint2D[200];
    }

    private void Update()
    {
        Debug.Log(this._grounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == (int) Mathf.Log(this.GroundLayer.value, 2)
            && collision.relativeVelocity.y > 0)
        {
            this._grounded = true;
            this.OnLanding.Invoke();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
        collision.GetContacts(this._colisionContacts);

        if (collision.collider.gameObject.layer == (int) Mathf.Log(this.GroundLayer.value, 2))
        {
            this._grounded = false;
        }
    }

    private void FlipSprite()
    {
        SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();
        renderer.flipX = this._facingRight;
        this._facingRight = !this._facingRight;
    }
}