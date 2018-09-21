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

    private Rigidbody2D Rigidbody;
    private CharacterController2D CharacterController;
    private float _currentDashDuration;
    private bool isDashing = false;
    public bool CanDash
    {
        get
        {
            return !this.isDashing;
        }
    }

    private float _baseLookAheadTime;

    public void Awake()
    {
        this.Rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        this.CharacterController = this.gameObject.GetComponent<CharacterController2D>();

        this.CameraParams = GameObject
            .FindGameObjectsWithTag("CinemachineCam")[0]
            .GetComponent<CinemachineVirtualCamera>()
            .GetComponentPipeline()[0]
            as CinemachineFramingTransposer;

        this._baseLookAheadTime = this.CameraParams.m_LookaheadTime;
    }

    public void LaunchSkills(InputsParameters inputs)
    {
        if (!this.isDashing && inputs.Dash)
        {
            this.isDashing = true;
            this.StartCoroutine("Dash");
        }
    }

    public IEnumerator Dash()
    {

        int dashDirection = this.CharacterController.direction;
        this.CameraParams.m_LookaheadTime = 0;
        this._currentDashDuration = 0;

        while (this._currentDashDuration < this.dashDuration + this.dashAddAnimationDuration)
        {
            if (this._currentDashDuration < this.dashDuration)
            {
                this.Rigidbody.velocity = new Vector2(this.dashforce * dashDirection, 0);
            }

            this._currentDashDuration += Time.fixedDeltaTime;
            yield return null;
        }

        this.StartCoroutine(Transition.Do.Lerp(new Ref<float>(this.CameraParams.m_LookaheadTime), this._baseLookAheadTime, 0.5f));

        this.OnDashEnd.Invoke();

        yield return new WaitForSeconds(this.dashCoolDown);
        this.isDashing = false;
    }


}
