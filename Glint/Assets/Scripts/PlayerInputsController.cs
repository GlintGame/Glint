using UnityEngine;

public class PlayerInputsController : MonoBehaviour
{
    [Range(0f, 1f)] public float walkSpeedLimit = 0.6f;

    private CharacterController2D Controller;
    private Animator PlayerAnimator;

    private float _absoluteSpeed;
    private InputsParameters _inputs;

    private void Awake()
    {
        this.Controller = this.gameObject.GetComponent<CharacterController2D>();
        this.PlayerAnimator = this.gameObject.GetComponent<Animator>();
        this._inputs = new InputsParameters();
    }

    private void FixedUpdate()
    {
        this._inputs.Jump = Input.GetButton("Jump");
        this._inputs.Run = Input.GetButton("Fire3");
        this._inputs.Dash = Input.GetAxis("RT") > 0.9;
        this._inputs.HorizontalMovement = Input.GetAxisRaw("Horizontal");

        if (this._inputs.Jump)
        {
            this.PlayerAnimator.SetBool("PlayerJump", true);
        }

        if (this._inputs.Dash)
        {
            this.PlayerAnimator.SetBool("PlayerDashing", true);
        }

        this._absoluteSpeed = Mathf.Abs(this._inputs.HorizontalMovement);
        this.PlayerAnimator.SetBool("PlayerRunning", this._absoluteSpeed > this.walkSpeedLimit && this._inputs.Run);
        this.PlayerAnimator.SetFloat("PlayerMovement", this._absoluteSpeed);

        this.Controller.Behave(this._inputs);
    }

    public void OnLanding()
    {
        this.PlayerAnimator.SetBool("PlayerJump", false);
        this.PlayerAnimator.SetBool("PlayerFalling", false);
    }

    public void OnFalling()
    {
        this.PlayerAnimator.SetBool("PlayerFalling", true);
    }

    public void OnDashEnd()
    {
        this.PlayerAnimator.SetBool("PlayerDashing", false);
    }
}