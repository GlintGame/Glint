using Interfaces.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Characters.Player.Skills
{
    [RequireComponent(typeof(PlayerSkills))]
    class Mele: MonoBehaviour, ISkill
    {
        // skill options
        public float MeleTimeBeforeHit = 0.2f;
        public float MeleTimeBetweenHits = 0.1f;
        public float MeleAttackCoolDown = 0.8f;
        public Vector2 MeleHitBoxOffset = new Vector2(3, 4);
        public Vector2 MeleHitboxSize = new Vector2(5, 6);
        public int MeleDamages = 15;
        public float PushForce = 5f;

        // needed components
        private PlayerSkills SkillManager;
        private CharacterController2D CharacterController;
        private Transform Transform;
        private Rigidbody2D Rigidbody;
        private PlayerStats PlayerStats;
        private Animator Animator;

        private void Awake()
        {
            this.SkillManager = this.GetComponent<PlayerSkills>();
            this.CharacterController = this.GetComponent<CharacterController2D>();
            this.Transform = this.GetComponent<Transform>();
            this.Rigidbody = this.GetComponent<Rigidbody2D>();
            this.PlayerStats = this.GetComponent<PlayerStats>();
            this.Animator = this.GetComponent<Animator>();
        }

        // 0: not attacking
        // 1: first attack
        // 2: second attack
        private uint _attackState = 0;

        public bool _isMeleAttacking;
        public bool CanNextAttack
        {
            get
            {
                return this._attackState == 1 || this._attackState == 0;
            }
        }

        // availability
        private bool _meleCooldown = false;
        private bool _canAttackMele
        {
            get
            {
                return
                    this.SkillManager.CanAct
                    && !this._meleCooldown;
            }
        }


        public bool PlayerCanAct()
        {
            return !this._isMeleAttacking;
        }

        public string GetInputName()
        {
            return "AttackOne";
        }

        public IEnumerator Launch()
        {
            if (!_canAttackMele)
                yield break;

            // initialisation
            this._isMeleAttacking = true;
            this.PlayerStats.IsInvicible = true;

            // animate
            this.Animator.SetBool("PlayerMele", true);

            // activation
            this.Rigidbody.velocity = Vector2.right * this.PushForce * this.CharacterController.Direction;
            yield return new WaitForSeconds(this.MeleTimeBeforeHit);

            Vector2 center = this.Transform.position;
            var offset = new Vector2(this.MeleHitBoxOffset.x * this.CharacterController.Direction, this.MeleHitBoxOffset.y);
            center += offset;

            Collider2D[] collided = Physics2D.OverlapAreaAll(center - this.MeleHitboxSize / 2, center + this.MeleHitboxSize / 2);

            foreach (Collider2D collider in collided)
            {
                IHitable target = collider.gameObject.GetComponent<IHitable>();
                if (collider.gameObject != this.gameObject && target != null)
                {
                    target.TakeDamages(this.MeleDamages, this.Transform.position);
                }
            }

            yield return new WaitForSeconds(this.MeleTimeBetweenHits);

            collided = Physics2D.OverlapAreaAll(center - this.MeleHitboxSize / 2, center + this.MeleHitboxSize / 2);

            foreach (Collider2D collider in collided)
            {
                IHitable target = collider.gameObject.GetComponent<IHitable>();
                if (collider.gameObject != this.gameObject && target != null)
                {
                    target.TakeDamages(this.MeleDamages, this.Transform.position);
                }
            }

            this._isMeleAttacking = false;
            
            this.Animator.SetBool("PlayerMele", false);
            this.PlayerStats.IsInvicible = false;

            // cooldown
            this._meleCooldown = true;
            yield return new WaitForSeconds(this.MeleAttackCoolDown);
            this._meleCooldown = false;
        }        
    }
}
