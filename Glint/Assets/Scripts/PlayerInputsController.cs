using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerInputsController : MonoBehaviour
{
    public CharacterController Controller;
    public float SpeedMultiplier = 30;
    public Animator PlayerAnimator;
    public CinemachineVirtualCamera CVCamera;

    private bool _jumping;
    private float _movementSpeed;
    private bool _running;
    private float _lookaheadTime;
    private CinemachineFramingTransposer body;

    void Awake()
    {
        // the body is the first array element cause it's the first in the unity editor...
        // and fuck thoses class names...
        this.body = this.CVCamera.GetComponentPipeline()[0] as CinemachineFramingTransposer;
        this._lookaheadTime = body.m_LookaheadTime;
    }

    void FixedUpdate()
    {
        this._jumping = Input.GetButtonDown("Jump");
        this._movementSpeed = Input.GetAxisRaw("Horizontal") * this.SpeedMultiplier;

        if (Input.GetButtonDown("Jump"))
        {
            this._jumping = true;
            this.PlayerAnimator.SetBool("PlayerJump", true);
            this.body.m_LookaheadTime = 0;
        }

        this._running = !(Input.GetButton("Fire3"));
        this.PlayerAnimator.SetBool("PlayerRunning", this._running);
        this.PlayerAnimator.SetFloat("PlayerMovement", Mathf.Abs(this._movementSpeed));

        Controller.Move(this._movementSpeed, this._jumping, this._running);
    }

    public void OnLanding()
    {
        this.PlayerAnimator.SetBool("PlayerJump", false);
        StartCoroutine("DoLerp");
    }

    IEnumerator DoLerp()
    {
        Debug.Log("lerping");
        while (this.body.m_LookaheadTime < this._lookaheadTime)
        {
            Debug.Log("lerping");
            this.body.m_LookaheadTime = Mathf.Lerp(this.body.m_LookaheadTime, this._lookaheadTime, 0.1f);
            yield return null;
        }        
    }
}