using UnityEngine;
using Luminosity.IO;
using Characters.Player.Skills;

public class PlayerInputsController : MonoBehaviour
{
    [Range(0f, 1f)] public float walkSpeedLimit = 0.6f;
    [Range(0f, 1f)] public float HorizontalDeadZone = 0.15f;

    private bool _jumpIsLock = false;

    private CharacterController2D Controller;
    private Animator PlayerAnimator;
    private Dash Dash;
    private ICharacterSkills CharacterSkills;

    private void Awake()
    {
        this.Controller = this.GetComponent<CharacterController2D>();
        this.PlayerAnimator = this.GetComponent<Animator>();
        this.Dash = this.GetComponent<Dash>();
        this.CharacterSkills = this.GetComponent<ICharacterSkills>();

        PauseMenu.OnPause += this.LockJump;
        PauseMenu.OnReleaseButton += this.UnlockJump;
        PauseMenu.OnResumeEscape += this.UnlockJump;
    }

    private void Update()
    {
        var inputs = new InputsParameters();

        if (!this._jumpIsLock)
        {
            if (InputManager.GetButtonDown("jump"))
            {
                this.Controller.StartJump();
            }
            else if (InputManager.GetButtonUp("jump"))
            {
                this.Controller.EndJump();
            }
        }

        bool isRunning = InputManager.GetAxis("run") > 0.9 || InputManager.GetButton("run");
        float horizontalMovement = InputManager.GetAxisRaw("lateral");
        // dead zone
        horizontalMovement = Mathf.Abs(horizontalMovement) > this.HorizontalDeadZone ? horizontalMovement : 0;

        if(horizontalMovement != 0 
            && Time.timeScale != 0 
            && this.Controller.Grounded)
        {
            AudioManager.Play("Marche");
        }
        else
        {
            AudioManager.Stop("Marche");
        }

        this.Controller.SetMove(
            horizontalMovement,
            isRunning
        );

        inputs.AttackOne = InputManager.GetButton("mele");
        inputs.AttackTwo = InputManager.GetButton("fireBall");
        inputs.Dash = this.Dash._canDash && ( InputManager.GetAxis("dash") > 0.9 || InputManager.GetButton("dash") );

        float absoluteSpeed = Mathf.Abs(horizontalMovement);
        this.PlayerAnimator.SetBool("PlayerRunning", absoluteSpeed > this.walkSpeedLimit && isRunning);
        this.PlayerAnimator.SetFloat("PlayerMovement", absoluteSpeed);

        this.CharacterSkills.LaunchSkills(inputs);
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

    public void OnDashStart()
    {
        this.PlayerAnimator.SetBool("PlayerDashing", true);
    }

    public void OnDashEnd()
    {
        this.PlayerAnimator.SetBool("PlayerDashing", false);
    }

    public void OnJumping()
    {
        this.PlayerAnimator.SetBool("PlayerJump", true);
    }


    private void LockJump()
    {
        this._jumpIsLock = true;
    }

    private void UnlockJump()
    {
        this._jumpIsLock = false;
    }
}