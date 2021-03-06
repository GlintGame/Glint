﻿using Interfaces.Player;
using System.Collections;
using UnityEngine;

namespace Characters.Player.Skills
{
    [RequireComponent(typeof(PlayerSkills))]
    class FireBall: MonoBehaviour, ISkill
    {
        // skill options
        public GameObject fireBallRef = null;
        public float FireBallTimeBeforeLaunch = 0.2f;
        public float FireBallCoolDown = 1f;
        public int FireBallDamage = 100;

        // needed components
        private PlayerSkills SkillManager;
        private Transform Transform;
        private Animator Animator;
        private void Awake()
        {
            this.SkillManager = this.GetComponent<PlayerSkills>();
            this.Transform = this.GetComponent<Transform>();
            this.Animator = this.GetComponent<Animator>();
        }

        // availability
        private bool _fireBallCoolDown = false;
        private bool _canAttackFireBall
        {
            get
            {
                return
                    this.SkillManager.CanAct
                    && !this._fireBallCoolDown;
            }
        }

        [HideInInspector]
        public bool _isFireBallAttacking;

        public bool PlayerCanAct()
        {
            return !this._isFireBallAttacking;
        }

        public string GetInputName()
        {
            return "AttackTwo";
        }

        public IEnumerator Launch()
        {
            Debug.Log(_canAttackFireBall);
            if (!_canAttackFireBall)
                yield break;

            // initialization
            this._isFireBallAttacking = true;

            // activation
            this.Animator.SetTrigger("PlayerFireball");
            yield return new WaitForSeconds(this.FireBallTimeBeforeLaunch);

            GameObject fireBall = Instantiate(this.fireBallRef);
            Vector3 origin = this.Transform.position;
            origin.y += 3.5f;
            fireBall.GetComponent<Transform>().position = origin;
            this._isFireBallAttacking = false;

            // cooldown
            this._fireBallCoolDown = true;
            yield return new WaitForSeconds(this.FireBallCoolDown);
            this._fireBallCoolDown = false;
        }
    }
}
