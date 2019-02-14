﻿using UnityEngine;
using Luminosity.IO;
using Characters.Player.Skills;
using Cinemachine;

public class PlayerInputsController : MonoBehaviour
{
    [Range(0f, 1f)] public float walkSpeedLimit = 0.6f;
    [Range(0f, 1f)] public float HorizontalDeadZone = 0.15f;

    private bool _jumpIsLock = false;

    private CharacterController2D Controller;
    private Animator PlayerAnimator;

    private float _absoluteSpeed;
    private InputsParameters _inputs;

    private Dash Dash;

    private void Awake()
    {
        this.Controller = this.GetComponent<CharacterController2D>();
        this.PlayerAnimator = this.GetComponent<Animator>();
        this.Dash = this.GetComponent<Dash>();
        this._inputs = new InputsParameters();

        PauseMenu.OnPause += this.LockJump;
        PauseMenu.OnReleaseButton += this.UnlockJump;
        PauseMenu.OnResumeEscape += this.UnlockJump;
    }

    private void Update()
    {
        if(!this._jumpIsLock)
        {
            this._inputs.StillJump = InputManager.GetButton("jump");
            this._inputs.Jump = InputManager.GetButtonDown("jump");
        }
        this._inputs.AttackOne = InputManager.GetButton("mele");
        this._inputs.AttackTwo = InputManager.GetButton("fireBall");
        this._inputs.Run = InputManager.GetAxis("run") > 0.9 || InputManager.GetButton("run");
        this._inputs.Dash = InputManager.GetAxis("dash") > 0.9 || InputManager.GetButton("dash");
        this._inputs.HorizontalMovement = InputManager.GetAxisRaw("lateral");
        this._inputs.HorizontalMovement = Mathf.Abs(this._inputs.HorizontalMovement) > this.HorizontalDeadZone ? this._inputs.HorizontalMovement : 0; // dead zone.

        if (this._inputs.StillJump)
        {
            this.PlayerAnimator.SetBool("PlayerJump", true);
        }
        

        if (this._inputs.Dash && this.Dash._canDash)
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


    private void LockJump()
    {
        this._jumpIsLock = true;
    }

    private void UnlockJump()
    {
        this._jumpIsLock = false;
    }
}