using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [Range(.2f, 1f)] public float WalkSpeedModifier = .8f;
    [Range(0, .3f)] public float MovementSmothing = .05f;

    [Range(0, 60f)] public float JumpForce = 35f;
    [Range(0, 8f)] public float fallingForce = 3;
    [Range(0, .3f)] public float AirMovementSmothing = .05f;

    public LayerMask GroundLayer;

    public UnityEvent OnLanding;
    
    private bool _grounded = true;
    private bool _facingRight = true;

    private Rigidbody2D RigidBody;
    private SpriteRenderer Renderer;
    

    public void Awake()
    {
        this.Renderer = this.GetComponent<SpriteRenderer>();
        this.RigidBody = this.GetComponent<Rigidbody2D>();
    }

    public void Move(float xspeed, bool jump, bool running)
    {
        if (!running)
        {
            xspeed *= this.WalkSpeedModifier;
        }
        Vector2 targetVelocity = new Vector2(xspeed, this.RigidBody.velocity.y);

        float currentSmoothing;
        if (this._grounded)
        {
            currentSmoothing = this.MovementSmothing;          
        }
        else
        {
            currentSmoothing = this.AirMovementSmothing;
        }

        this.RigidBody.velocity = Vector2.Lerp(this.RigidBody.velocity, targetVelocity, currentSmoothing);
        
        if (jump && this._grounded)
        {
            this.RigidBody.velocity = new Vector2(this.RigidBody.velocity.x, this.JumpForce);
            this._grounded = false;
        }
        else if(!(jump || this._grounded) || this.RigidBody.velocity.y < 0)
        {
            this.RigidBody.velocity += Vector2.up * Physics2D.gravity.y * this.fallingForce * Time.fixedDeltaTime;
        }

        this.FlipSprite(xspeed);
    }     

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == (int) Mathf.Log(this.GroundLayer.value, 2))
        {

            bool collidedOnlyOnBottom = false;
            foreach(ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.01)
                {
                    collidedOnlyOnBottom = true;
                }
            }
            
            if(collidedOnlyOnBottom)
            {
                this._grounded = true;
                this.OnLanding.Invoke();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {        

        if (collision.collider.gameObject.layer == (int) Mathf.Log(this.GroundLayer.value, 2))
        {
            this._grounded = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!this._grounded)
        {
            OnCollisionEnter2D(collision);
        }
    }

    private void FlipSprite(float xspeed)
    {
        if (xspeed < 0 && _facingRight
            || xspeed > 0 && !_facingRight)
        {            
            this.Renderer.flipX = this._facingRight;
            this._facingRight = !this._facingRight;
        }        
    }
}