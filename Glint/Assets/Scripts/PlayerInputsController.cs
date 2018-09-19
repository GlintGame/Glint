using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerInputsController : MonoBehaviour
{

    private CharacterController2D Controller;
    private Animator PlayerAnimator;

    private bool _jumping;
    private float _movementSpeed;
    private float _absoluteSpeed;
    private bool _running;

    void Awake()
    {
        this.Controller = gameObject.GetComponent<CharacterController2D>();
        this.PlayerAnimator = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        this._jumping = Input.GetButton("Jump");
        this._running = Input.GetButton("Fire3");
        this._movementSpeed = Input.GetAxisRaw("Horizontal");

        if (this._jumping)
        {
            this.PlayerAnimator.SetBool("PlayerJump", true);
        }

        this._absoluteSpeed = Mathf.Abs(this._movementSpeed);
        this.PlayerAnimator.SetBool("PlayerRunning", this._absoluteSpeed > 0.6 && this._running);
        this.PlayerAnimator.SetFloat("PlayerMovement", this._absoluteSpeed);

        this.Controller.Move(this._movementSpeed, this._jumping, this._running);
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