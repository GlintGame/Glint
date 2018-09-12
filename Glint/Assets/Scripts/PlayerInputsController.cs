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


	void Update ()
	{
	    this._jumping = Input.GetButtonDown("Jump");
	    this._movementSpeed = Input.GetAxisRaw("Horizontal") * this.SpeedMultiplier;

	    if (Input.GetButtonDown("Jump"))
	    {
	        this._jumping = true;
	        this.PlayerAnimator.SetBool("PlayerJump", true);
        }
        
        this.PlayerAnimator.SetFloat("PlayerMovement", this._movementSpeed);
	}

    void FixedUpdate()
    {
        Controller.Move(_movementSpeed, _jumping, true);
    }

    public void OnLanding()
    {
        this.PlayerAnimator.SetBool("PlayerJump", false);
    }
}
