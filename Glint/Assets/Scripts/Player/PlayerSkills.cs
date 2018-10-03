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

    public float MeleTimeBeforeHit = 0.2f;
    public float MeleAttackCoolDown = 0.8f;
    public float MeleHitboxDistance = 3;
    public Vector2 MeleHitboxSize = new Vector2(10,5);
    public int MeleDamages = 5;

    private CinemachineFramingTransposer CameraParams;
    public UnityEvent OnDashEnd;
    private Animator PlayerAnimator;

    private Rigidbody2D Rigidbody;
    private CharacterController2D CharacterController;
    private Transform Transform;
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
    private bool CanAttackMele = true;

    private float _baseLookAheadTime;

    public void Awake()
    {
        this.Rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        this.CharacterController = this.gameObject.GetComponent<CharacterController2D>();
        this.PlayerAnimator = this.gameObject.GetComponent<Animator>();
        this.Transform = gameObject.GetComponent<Transform>();

        // getting cinemachine component
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
            this.StartCoroutine(Dash());
        }

        if (this.CanAttackMele
            && inputs.AttackOne)
        {
            this.StartCoroutine(Mele());
        }
    }

    public IEnumerator Mele()
    {
        // initialisation
        this.CanAttackMele = false;

        // activation
        yield return new WaitForSeconds(this.MeleTimeBeforeHit);
        Vector2 center = this.transform.position;
        center.x += this.CharacterController.direction * this.MeleHitboxDistance;
        Collider2D[] collided = Physics2D.OverlapAreaAll(center + this.MeleHitboxSize/2, center - this.MeleHitboxSize/2);

        foreach (Collider2D collider in collided)
        {
            IHitable hitableObject;
            if ((hitableObject = collider.gameObject.GetComponent<IHitable>()) != null)
            {
                hitableObject.TakeDamages(this.MeleDamages, this.transform.position);
            }
        }


        // cooldown
        yield return new WaitForSeconds(this.MeleAttackCoolDown);
        this.CanAttackMele = true;
    }

    public IEnumerator Dash()
    {
        // initialisation
        this._isDashing = true;
        int dashDirection = this.CharacterController.direction;
        this.CameraParams.m_LookaheadTime = 0;
        this._currentDashDuration = 0;
        this._overallDashDuration = this.dashDuration + this.dashAddAnimationDuration;
        this.PlayerAnimator.SetFloat("DashSpeed", 1 / this._overallDashDuration);

        // activation
        while (this._currentDashDuration < this._overallDashDuration)
        {
            if (this._currentDashDuration < this.dashDuration)
            {
                this.Rigidbody.velocity = new Vector2(this.dashforce * dashDirection, 0);
            }

            this._currentDashDuration += Time.fixedDeltaTime;
            yield return null;
        }

        // cooldown
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
