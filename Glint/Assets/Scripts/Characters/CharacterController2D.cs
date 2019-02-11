using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [Range(20f, 40f)] public float SpeedMultiplier = 30;
    [Range(.2f, 1f)] public float WalkSpeedModifier = .8f;
    [Range(0, .3f)] public float MovementSmothing = .05f;

    [Range(0, 60f)] public float JumpForce = 35f;
    [Range(0, 8f)] public float fallingForce = 3;
    [Range(0, .3f)] public float AirMovementSmothing = .05f;

    public LayerMask GroundLayer;

    public UnityEvent OnLanding;
    public UnityEvent OnFalling;

    private bool _grounded = true;
    public bool IsInAir
    {
        get
        {
            return !this._grounded;
        }
    }

    private bool _facingRight = true;
    public int direction
    {
        get
        {
            return this._facingRight ? 1 : -1;
        }
    }

    private Rigidbody2D RigidBody;
    private SpriteRenderer Renderer;
    private ICharacterSkills CharacterSkills;

    public void Awake()
    {
        this.Renderer = this.GetComponent<SpriteRenderer>();
        this.RigidBody = this.GetComponent<Rigidbody2D>();
        this.CharacterSkills = this.GetComponent<ICharacterSkills>();
    }

    public void Behave(InputsParameters inputs)
    {
        float xspeed = inputs.HorizontalMovement;

        xspeed *= inputs.Run ? this.SpeedMultiplier : this.SpeedMultiplier * this.WalkSpeedModifier;

        var targetVelocity = new Vector2(xspeed, this.RigidBody.velocity.y);

        float currentSmoothing = this._grounded ? this.MovementSmothing : this.AirMovementSmothing;

        this.RigidBody.velocity = Vector2.Lerp(this.RigidBody.velocity, targetVelocity, currentSmoothing);

        if (inputs.Jump && this._grounded)
        {
            this.RigidBody.velocity = new Vector2(this.RigidBody.velocity.x, this.JumpForce);
            this._grounded = false;
        }
        else if (!(inputs.Jump || this._grounded) || this.RigidBody.velocity.y < -10)
        {
            this.OnFalling.Invoke();
            this.RigidBody.velocity += Vector2.up * Physics2D.gravity.y * this.fallingForce * Time.fixedDeltaTime;
        }

        this.FlipSprite(xspeed);
        this.CharacterSkills.LaunchSkills(inputs);
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
                this._grounded = true;
                this.OnLanding.Invoke();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == (int)Mathf.Log(this.GroundLayer.value, 2))
        {
            this._grounded = false;
            if (collision.otherRigidbody.velocity.y < -0.8f)
            {
                this.OnFalling.Invoke();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!this._grounded)
        {
            this.OnCollisionEnter2D(collision);
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
}