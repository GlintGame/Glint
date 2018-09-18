using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerInputsController : MonoBehaviour
{
    public CharacterController2D Controller;
    public float SpeedMultiplier = 30;
    public Animator PlayerAnimator;
    public CinemachineVirtualCamera CVCamera;

    private bool _jumping;
    private float _movementSpeed;
    private bool _running;
    private float _lookaheadTime;

    void Awake()
    {
        // the body is the first array element cause it's the first in the unity editor...
        // and fuck thoses class names...
    }

    void FixedUpdate()
    {
        this._jumping = Input.GetButton("Jump");
        this._running = !(Input.GetButton("Fire3"));

        if (Input.GetButton("Jump"))
        {
            this._jumping = true;
            this.PlayerAnimator.SetBool("PlayerJump", true);
        }

        this._movementSpeed = Input.GetAxisRaw("Horizontal");

        this.PlayerAnimator.SetBool("PlayerRunning", this._running);
        this.PlayerAnimator.SetFloat("PlayerMovement", Mathf.Abs(this._movementSpeed));

        this._movementSpeed *= this.SpeedMultiplier;

        Controller.Move(this._movementSpeed, this._jumping, this._running);
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
}