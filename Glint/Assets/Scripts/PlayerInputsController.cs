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
        this._movementSpeed = Input.GetAxisRaw("Horizontal") * this.SpeedMultiplier;

        if (Input.GetButton("Jump"))
        {
            this._jumping = true;
            this.PlayerAnimator.SetBool("PlayerJump", true);
        }
        
        this.PlayerAnimator.SetBool("PlayerRunning", this._running);
        this.PlayerAnimator.SetFloat("PlayerMovement", Mathf.Abs(this._movementSpeed));

        Controller.Move(this._movementSpeed, this._jumping, this._running);
    }

    public void OnLanding()
    {
        this.PlayerAnimator.SetBool("PlayerJump", false);
    }   
}