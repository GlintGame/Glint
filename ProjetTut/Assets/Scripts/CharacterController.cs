using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Range(.2f, 1f)] public float WalkSpeedModifier = .8f;
    [Range(.2f, 1f)] public float InAirSpeedModifier = .6f;
    public Rigidbody2D RigidBody;
    [Range(0, 2000f)] public float JumpForce = 700f;
    public LayerMask GroundLayer;
    public Transform GroundCheck;

    [Range(0, .3f)] public float MovementSmothing = .05f;

    private Vector3 _velocity = Vector3.zero;
    private bool _grounded = true;
    const float GroundedRadius = 0.5f;

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

        Vector3 targetVelocity = new Vector2(xspeed, RigidBody.velocity.y);
        this.RigidBody.velocity = Vector3.SmoothDamp(this.RigidBody.velocity, targetVelocity, ref this._velocity, this.MovementSmothing);


        if (jump && this._grounded)
        {
            var jumpVector = new Vector2(0.0f, this.JumpForce);
            this.RigidBody.AddForce(jumpVector);

            this._grounded = false;
        }
    }

    void FixedUpdate()
    {
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
    }
}