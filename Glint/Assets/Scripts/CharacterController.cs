using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
    [Range(.2f, 1f)] public float WalkSpeedModifier = .8f;
    [Range(0, .3f)] public float MovementSmothing = .05f;
    [Range(0, 60f)] public float JumpForce = 35f;
    public Rigidbody2D RigidBody;
    public LayerMask GroundLayer;
    public float AirVelocityToForceFactor = 50;

    public UnityEvent OnLanding;
    
    private Vector3 _velocity = Vector3.zero;
    private bool _grounded = true;
    private bool _facingRight = true;
    private bool _jumpingRight = false;
    

    public void Move(float xspeed, bool jump, bool running)
    {

        Vector3 targetVelocity = new Vector2(xspeed, this.RigidBody.velocity.y);
        if (!running)
        {
            targetVelocity.x *= this.WalkSpeedModifier;
        }

        if (this._grounded)
        {
            this.RigidBody.velocity = Vector3.SmoothDamp(this.RigidBody.velocity, targetVelocity, ref this._velocity, this.MovementSmothing);

        }
        else if ((xspeed > 0 && !this._jumpingRight)
                || (xspeed < 0 && this._jumpingRight)) 
        {          
            // in air there is the body inertia, so we cant just overide the old velocity
            targetVelocity.x *= this.AirVelocityToForceFactor;
            this.RigidBody.AddForce(targetVelocity);
        }        

        if (xspeed < 0 && _facingRight)
        {
            this.FlipSprite();
        }
        else if (xspeed > 0 && !_facingRight)
        {
            this.FlipSprite();
        }

        
        // jump mechanic
        if (jump && this._grounded)
        {
            var jumpVector = new Vector2(this.RigidBody.velocity.x , this.JumpForce);
            this.RigidBody.velocity = jumpVector;

            this._grounded = false;
            this._jumpingRight = this._facingRight;
        }        
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

        if (collision.collider.gameObject.layer == (int) Mathf.Log(this.GroundLayer.value, 2))
        {
            this._grounded = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!this._grounded)
        {
            if (collision.collider.gameObject.layer == (int)Mathf.Log(this.GroundLayer.value, 2)
            && collision.relativeVelocity.y > 0)
            {
                this._grounded = true;
            }
        }
    }

    private void FlipSprite()
    {
        SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();
        renderer.flipX = this._facingRight;
        this._facingRight = !this._facingRight;
    }
}