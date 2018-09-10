using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerInputsController : MonoBehaviour
{

    public CharacterController Controller;
    public float SpeedMultiplier = 30;

    private bool _jumping;
    private float _movementSpeed;


	void Update ()
	{
	    this._jumping = Input.GetButtonDown("Jump");
	    this._movementSpeed = Input.GetAxisRaw("Horizontal") * this.SpeedMultiplier;
	}

    void FixedUpdate()
    {
        Controller.Move(_movementSpeed, _jumping, true);
    }
}
