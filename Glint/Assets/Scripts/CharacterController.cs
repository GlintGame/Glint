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
    [Range(0, 40f)] public float JumpForce = 20f;
    public Rigidbody2D RigidBody;
    public LayerMask GroundLayer;
    public Transform GroundCheck;

    public UnityEvent OnLanding;
    
    private Vector3 _velocity = Vector3.zero;
    private bool _grounded = true;
    private bool _facingRight = true;
    private const float GroundedRadius = 1f;

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

    void FixedUpdate()
    {
        // test if on ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, GroundLayer);

        bool IsGrounded = false;
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != this.gameObject)
            {
                IsGrounded = true;                
            }
        }

        this._grounded = IsGrounded;
        if (IsGrounded)
        {            
            this.OnLanding.Invoke();
        }
    }

    private void FlipSprite()
    {

        SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();
        renderer.flipX = this._facingRight;
        this._facingRight = !this._facingRight;
    }
}