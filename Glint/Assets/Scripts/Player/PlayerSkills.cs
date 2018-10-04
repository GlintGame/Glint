using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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

    public GameObject fireBallRef;
    public float FireBallTimeBeforeLaunch = 0.2f;
    public float FireBallCoolDown = 1f;
    public int FireBallDamage = 100;

    private CinemachineFramingTransposer CameraParams;
    public UnityEvent OnDashEnd;
    private Animator PlayerAnimator;

    private Rigidbody2D Rigidbody;
    private CharacterController2D CharacterController;
    private Transform Transform;
    private float _baseLookAheadTime;

    private float _currentDashDuration; // could be removed using the utils.ref class
    private float _overallDashDuration;

    // maybe create an object to handle cooldowns and skills
    public bool CanAct
    {
        get
        {
            return
                !this._isAttacking
                && !this._isDashing;
        }
    }

    //dash
    private bool _isDashing = false;
    private bool _dashCoolDown = false;
    private bool _hasDashInAir = false;
    public bool _canDash
    {
        get
        {
            return
                !this._isDashing
                && !this._dashCoolDown
                && !(this.CharacterController.IsInAir
                        && this._hasDashInAir);
        }
    }
    
    // attacks
    private bool _isAttacking = false;

    // mele
    private bool _meleCooldown = false;
    private bool _canAttackMele
    {
        get
        {
            return
                !this._isAttacking 
                && !this._meleCooldown;
        }
    }

    // fireBall

    private bool _fireBallCoolDown = false;
    private bool _canAttackFireBall
    {
        get
        {
            return
                !this._isAttacking
                && !this._fireBallCoolDown;
        }
    }

    

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
        if (this.CanAct)
        {
            if (this._canDash
            && inputs.Dash)
            {
                this.StartCoroutine(Dash());
            }

            if (this._canAttackMele
                && inputs.AttackOne)
            {
                this.StartCoroutine(Mele());
            }

            if (this._canAttackFireBall
                && inputs.AttackTwo)
            {
                this.StartCoroutine(FireBall());
            }
        }
    }

    private IEnumerator Mele()
    {
        // initialisation
        this._isAttacking = true;

        // activation
        yield return new WaitForSeconds(this.MeleTimeBeforeHit);
        Vector2 center = this.Transform.position;
        center.x += this.CharacterController.direction * this.MeleHitboxDistance;
        Collider2D[] collided = Physics2D.OverlapAreaAll(center + this.MeleHitboxSize/2, center - this.MeleHitboxSize/2);

        foreach (Collider2D collider in collided)
        {
            IHitable hitableObject;
            if ((hitableObject = collider.gameObject.GetComponent<IHitable>()) != null)
            {
                hitableObject.TakeDamages(this.MeleDamages, this.Transform.position);
            }
        }

        this._isAttacking = false;

        // cooldown
        this._meleCooldown = true;
        yield return new WaitForSeconds(this.MeleAttackCoolDown);
        this._meleCooldown = false;
    }

    private IEnumerator Dash()
    {
        // initialisation
        this._isDashing = true;
        int dashDirection = this.CharacterController.direction;
        
        this._currentDashDuration = 0;
        this._overallDashDuration = this.dashDuration + this.dashAddAnimationDuration;
        this.PlayerAnimator.SetFloat("DashSpeed", 1 / this._overallDashDuration);

            // camera shit
        this.CameraParams.m_LookaheadTime = 0;

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
        this._isDashing = false;

        // cooldown
            // camera shit again
        this.StartCoroutine(utils.Coroutine.Do.Lerp(new utils.Ref<float>(this.CameraParams.m_LookaheadTime), this._baseLookAheadTime, 0.5f));

        if (this.CharacterController.IsInAir)
        {
            this._hasDashInAir = true;
        }
        this.OnDashEnd.Invoke();

        this._dashCoolDown = true;
        yield return new WaitForSeconds(this.dashCoolDown);
        this._dashCoolDown = false;

    }

    private IEnumerator FireBall()
    {
        // initialization
        this._isAttacking = true;

        // activation
        yield return new WaitForSeconds(this.FireBallTimeBeforeLaunch);
        GameObject fireBall = Instantiate(this.fireBallRef);
        fireBall.SetActive(true);
        Vector3 origin = this.Transform.position;
        origin.y += 3.5f;
        fireBall.GetComponent<Transform>().position = origin;
        this._isAttacking = false;

        // cooldown
        this._fireBallCoolDown = true;
        yield return new WaitForSeconds(this.FireBallCoolDown);
        this._fireBallCoolDown = false;
    }

    public void OnLanding()
    {
        this._hasDashInAir = false;
    }
}
