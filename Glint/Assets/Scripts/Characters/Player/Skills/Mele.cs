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
        public float MeleAttackCoolDown = 0.8f;
        public float MeleHitboxDistance = 3;
        public Vector2 MeleHitboxSize = new Vector2(10, 5);
        public int MeleDamages = 5;

        // needed components
        private PlayerSkills SkillManager;
        private CharacterController2D CharacterController;
        private Transform Transform;
        private void Awake()
        {
            this.SkillManager = this.GetComponent<PlayerSkills>();
            this.CharacterController = this.GetComponent<CharacterController2D>();
            this.Transform = this.GetComponent<Transform>();
        }

        public bool _isMeleAttacking;

        // availability
        private bool _meleCooldown = false;
        private bool _canAttackMele
        {
            get
            {
                return
                    !this.SkillManager.CanAct
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
            // initialisation
            this._isMeleAttacking = true;

            // activation
            yield return new WaitForSeconds(this.MeleTimeBeforeHit);
            Vector2 center = this.Transform.position;
            center.x += this.CharacterController.direction * this.MeleHitboxDistance;
            Collider2D[] collided = Physics2D.OverlapAreaAll(center + this.MeleHitboxSize / 2, center - this.MeleHitboxSize / 2);

            foreach (Collider2D collider in collided)
            {
                IHitable target = collider.gameObject.GetComponent<IHitable>();
                if (collider.gameObject != this.gameObject && target != null)
                {
                    target.TakeDamages(this.MeleDamages, this.Transform.position);
                }
            }

            this._isMeleAttacking = false;

            // cooldown
            this._meleCooldown = true;
            yield return new WaitForSeconds(this.MeleAttackCoolDown);
            this._meleCooldown = false;
        }        
    }
}
