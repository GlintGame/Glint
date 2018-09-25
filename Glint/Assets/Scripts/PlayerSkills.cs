using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using utils;

public class PlayerSkills : MonoBehaviour, ICharacterSkills
{
    [Range(0f, 1.5f)] public float dashDuration = 0.6f;
    [Range(0f, 1.5f)] public float dashAddAnimationDuration = 0.6f;
    public float dashforce = 2.5f;
    public float dashCoolDown = 2f;

    private CinemachineFramingTransposer CameraParams;
    public UnityEvent OnDashEnd;
    private Animator PlayerAnimator;

    private Rigidbody2D Rigidbody;
    private CharacterController2D CharacterController;
    private float _currentDashDuration;
    private float _overallDashDuration;
    private bool _isDashing = false;
    private bool _hasDashInAir = false;
    public bool CanDash
    {
        get
        {
            return 
                !(this._isDashing
                || (this.CharacterController.IsInAir
                    && this._hasDashInAir));
        }
    }

    private float _baseLookAheadTime;

    public void Awake()
    {
        this.Rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        this.CharacterController = this.gameObject.GetComponent<CharacterController2D>();
        this.PlayerAnimator = this.gameObject.GetComponent<Animator>();

        this.CameraParams = GameObject
            .FindGameObjectsWithTag("CinemachineCam")[0]
            .GetComponent<CinemachineVirtualCamera>()
            .GetComponentPipeline()[0]
            as CinemachineFramingTransposer;

        this._baseLookAheadTime = this.CameraParams.m_LookaheadTime;
    }

    public void LaunchSkills(InputsParameters inputs)
    {
        if (this.CanDash
            && inputs.Dash)
        {
            this._isDashing = true;
            this.StartCoroutine("Dash");
        }
    }

    public IEnumerator Dash()
    {
        int dashDirection = this.CharacterController.direction;
        this.CameraParams.m_LookaheadTime = 0;
        this._currentDashDuration = 0;
        this._overallDashDuration = this.dashDuration + this.dashAddAnimationDuration;
        this.PlayerAnimator.SetFloat("DashSpeed", 1 / this._overallDashDuration);

        while (this._currentDashDuration < this._overallDashDuration)
        {
            if (this._currentDashDuration < this.dashDuration)
            {
                this.Rigidbody.velocity = new Vector2(this.dashforce * dashDirection, 0);
            }

            this._currentDashDuration += Time.fixedDeltaTime;
            yield return null;
        }

        this.StartCoroutine(Transition.Do.Lerp(new Ref<float>(this.CameraParams.m_LookaheadTime), this._baseLookAheadTime, 0.5f));
        if (this.CharacterController.IsInAir)
        {
            this._hasDashInAir = true;
        }
        this.OnDashEnd.Invoke();

        yield return new WaitForSeconds(this.dashCoolDown);
        this._isDashing = false;

    }

    public void OnLanding()
    {
        this._hasDashInAir = false;
    }
}
