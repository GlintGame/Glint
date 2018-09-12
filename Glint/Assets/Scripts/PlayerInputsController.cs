using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerInputsController : MonoBehaviour
{
    public CharacterController Controller;
    public float SpeedMultiplier = 30;
    public Animator PlayerAnimator;

    private bool _jumping;
    private float _movementSpeed;
    private bool _running;


    void Update()
    {
        this._jumping = Input.GetButtonDown("Jump");
        this._movementSpeed = Input.GetAxisRaw("Horizontal") * this.SpeedMultiplier;

        if (Input.GetButtonDown("Jump"))
        {
            this._jumping = true;
            this.PlayerAnimator.SetBool("PlayerJump", true);
        }

        this._running = !(Input.GetButton("Fire3"));
        this.PlayerAnimator.SetBool("PlayerRunning", this._running);
        this.PlayerAnimator.SetFloat("PlayerMovement", Mathf.Abs(this._movementSpeed));
    }

    void FixedUpdate()
    {
        Controller.Move(this._movementSpeed, this._jumping, this._running);
    }

    public void OnLanding()
    {
        this.PlayerAnimator.SetBool("PlayerJump", false);
    }
}