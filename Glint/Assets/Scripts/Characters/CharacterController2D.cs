using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    // components params
    [Range(20f, 40f)] public float SpeedMultiplier = 30;
    [Range(.2f, 1f)] public float WalkSpeedModifier = .8f;
    [Range(0, .3f)] public float MovementSmothing = .05f;

    [Range(0, 60f)] public float JumpForce = 35f;
    [Range(0, 8f)] public float FallingForce = 3;
    [Range(0, .3f)] public float AirMovementSmothing = .05f;

    [Range(0f, 10f)] public float GroundRaycastsWidth = 1.5f;
    [Range(-5f, 5f)] public float GroundRaycastOrigin = -0.5f;
    [Range(-5f, 5f)] public float GroundRaycastDist = 1f;

    public LayerMask GroundLayer;

    public UnityEvent OnLanding;
    public UnityEvent OnFalling;
    public UnityEvent OnLeaveGround;

    private bool grounded;
    public bool Grounded
    {
        get { return this.grounded; }
        set
        {
            if(value)   this.OnLanding.Invoke();
            else        this.OnLeaveGround.Invoke();

            this.grounded = value;
        }
    }

    private bool _jumpingInput = false;
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
    private Transform Transform;

    public void Awake()
    {
        this.Renderer = this.GetComponent<SpriteRenderer>();
        this.RigidBody = this.GetComponent<Rigidbody2D>();
        this.Transform = this.GetComponent<Transform>();
    }

    // incoming Events triggers
    public void StartJump()
    {
        if (this.Grounded)
        {
            this.RigidBody.velocity = new Vector2(this.RigidBody.velocity.x, this.JumpForce);
            this._jumpingInput = true;
        }
    }

    public void EndJump()
    {
        this._jumpingInput = false;
    }

    public void Move(float hMove, bool isRunning)
    {
        this._horizintalTargetSpeed = hMove * this.SpeedMultiplier;

        if (!isRunning)
        {
            this._horizintalTargetSpeed *= this.WalkSpeedModifier;
        }

        this.FlipSprite(hMove);
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
        this.Grounded = this.CheckGround();

        this.RigidBody.velocity = Vector2.Lerp(
                this.RigidBody.velocity, 
                this._frameVelocity,
                this._currentSmooting
            );
        
        // falling mass acceleration
        if ((!this.Grounded && !this._jumpingInput) || this.RigidBody.velocity.y < -10)
        {
            this.OnFalling.Invoke();
            this.RigidBody.velocity += Vector2.up * Physics2D.gravity.y * this.FallingForce * Time.fixedDeltaTime;
        }
    }

    private bool CheckGround()
    {
        Vector2 rightCheckOrigin = (Vector2)this.Transform.position - new Vector2(this.GroundRaycastsWidth / 2, this.GroundRaycastOrigin);
        Vector2 leftCheckOrigin = (Vector2)this.Transform.position - new Vector2(-this.GroundRaycastsWidth / 2, this.GroundRaycastOrigin);
        
        Debug.DrawLine(
            rightCheckOrigin,
            rightCheckOrigin - new Vector2(0, this.GroundRaycastDist)
        );
        Debug.DrawLine(
            leftCheckOrigin,
            leftCheckOrigin - new Vector2(0, this.GroundRaycastDist)
        );

        RaycastHit2D rightRay =  Physics2D.Raycast(
            rightCheckOrigin,
            Vector2.down,
            this.GroundRaycastDist,
            this.GroundLayer
        );

        RaycastHit2D leftRay = Physics2D.Raycast(
            leftCheckOrigin,
            Vector2.down,
            this.GroundRaycastDist,
            this.GroundLayer
        );

        return rightRay || leftRay;
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
}