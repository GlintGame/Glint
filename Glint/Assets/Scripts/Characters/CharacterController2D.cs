using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    // components params
    [Range(20f, 40f)] public float SpeedMultiplier = 30;
    [Range(.2f, 1f)] public float WalkSpeedModifier = .8f;
    [Range(0, .3f)] public float MovementSmothing = .05f;

    [Range(0, 60f)] public float JumpForce = 35f;
    [Range(0, 8f)] public float fallingForce = 3;
    [Range(0, .3f)] public float AirMovementSmothing = .05f;

    public LayerMask GroundLayer;

    public UnityEvent OnLanding;
    public UnityEvent OnFalling;
    public UnityEvent OnJumping;

    public bool Grounded { get; private set; }
    private bool _jumping = false;
    private float _horizintalTargetSpeed = 0f;

    private bool _facingRight = true;
    public int Direction
    {
        get
        {
            return this._facingRight ? 1 : -1;
        }
    }

    private float _currentSmooting
    {
        get
        {
            return this.Grounded ? this.MovementSmothing : this.AirMovementSmothing;
        }
    }

    // component references and init
    private Rigidbody2D RigidBody;
    private SpriteRenderer Renderer;

    public void Awake()
    {
        this.Renderer = this.GetComponent<SpriteRenderer>();
        this.RigidBody = this.GetComponent<Rigidbody2D>();
    }

    // incoming Events triggers
    public void StartJump()
    {
        if (this.Grounded)
        {
            this.RigidBody.velocity = new Vector2(this.RigidBody.velocity.x, this.JumpForce);
            this._jumping = true;
            this.Grounded = false;
            this.OnJumping.Invoke();
        }
    }

    public void EndJump()
    {
        this._jumping = false;
    }

    public void Move(float speed, bool isRunning)
    {
        this._horizintalTargetSpeed = speed * this.SpeedMultiplier;

        if (!isRunning)
        {
            this._horizintalTargetSpeed *= this.WalkSpeedModifier;
        }

        this.FlipSprite(speed);
    }

    // velocity wrapper
    private Vector2 _frameVelocity {
        get
        {
            return new Vector2(this._horizintalTargetSpeed, this.RigidBody.velocity.y);
        }
    }

    // physics update
    private void FixedUpdate()
    {
        this.RigidBody.velocity = Vector2.Lerp(
                this.RigidBody.velocity, 
                this._frameVelocity, 
                this._currentSmooting
            );
        
        // falling mass acceleration
        if (!(!this.Grounded && this._jumping) && this.RigidBody.velocity.y < -10)
        {
            this.OnFalling.Invoke();
            this.RigidBody.velocity += Vector2.up * Physics2D.gravity.y * this.fallingForce * Time.fixedDeltaTime;
        }
    }

    private void FlipSprite(float xspeed)
    {
        if (xspeed < 0 && this._facingRight
            || xspeed > 0 && !this._facingRight)
        {
            this.Renderer.flipX = this._facingRight;
            this._facingRight = !this._facingRight;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == (int)Mathf.Log(this.GroundLayer.value, 2))
        {
            bool collidedOnlyOnBottom = false;
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.3)
                {
                    collidedOnlyOnBottom = true;
                }
            }

            if (collidedOnlyOnBottom)
            {
                this.Grounded = true;
                this.OnLanding.Invoke();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == (int)Mathf.Log(this.GroundLayer.value, 2))
        {
            this.Grounded = false;
            if (collision.otherRigidbody.velocity.y < -0.8f)
            {
                this.OnFalling.Invoke();
            }
        }
    }
}