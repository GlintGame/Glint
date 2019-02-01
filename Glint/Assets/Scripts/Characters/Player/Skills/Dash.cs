using System.Collections;
using UnityEngine;
using Interfaces.Player;
using UnityEngine.Events;

namespace Characters.Player.Skills
{
    [RequireComponent(typeof(PlayerSkills))]
    class Dash : MonoBehaviour, ISkill
    {
        // skill options
        [Range(0f, 1.5f)] public float dashDuration = 0.2f;
        [Range(0f, 1.5f)] public float dashAddAnimationDuration = 0.08f;
        public float dashforce = 55f;
        public float dashCoolDown = 0.65f;

        // needed components
        private CharacterController2D CharacterController;
        private Animator PlayerAnimator;
        private Rigidbody2D Rigidbody;
        private void Awake()
        {
            this.CharacterController = this.GetComponent<CharacterController2D>();
            this.PlayerAnimator = this.GetComponent<Animator>();
            this.Rigidbody = this.GetComponent<Rigidbody2D>();
        }

        // giving events
        public UnityEvent OnDashEnd;

        // given events
        public void OnLanding()
        {
            this._hasDashInAir = false;
        }

        // variables
        private float _currentDashDuration; // could be removed using the utils.ref class
        private float _overallDashDuration;


        // availability
        private bool _isDashing = false;
        private bool _dashCoolDown = false;
        private bool _hasDashInAir = false;
        public bool _canDash
        {
            get
            {

                return
                    // si le joueur
                    // ne dash pas
                    !this._isDashing
                    // n'est pas en cool down
                    && !this._dashCoolDown
                    // n'est pas en l'air alors qu'il a déjà dashé
                    && !(this.CharacterController.IsInAir && this._hasDashInAir);
            }
        }

        public bool PlayerCanAct()
        {
            return !this._isDashing && !this._dashCoolDown;
        }

        public string GetInputName()
        {
            return "Dash";
        }

        public IEnumerator Launch()
        {
            // initialisation
            this._isDashing = true;
            int dashDirection = this.CharacterController.direction;

            this._currentDashDuration = 0;
            this._overallDashDuration = this.dashDuration + this.dashAddAnimationDuration;
            this.PlayerAnimator.SetFloat("DashSpeed", 1 / this._overallDashDuration);

            this.StartCoroutine(utils.Coroutine.Do.StopLookAhead(this._overallDashDuration * 2));

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
            if (this.CharacterController.IsInAir)
            {
                this._hasDashInAir = true;
            }

            this.OnDashEnd.Invoke();

            this._dashCoolDown = true;
            yield return new WaitForSeconds(this.dashCoolDown);
            this._dashCoolDown = false;
        }
    }
}
