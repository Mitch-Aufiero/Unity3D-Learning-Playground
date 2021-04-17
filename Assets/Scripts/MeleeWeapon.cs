using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class MeleeWeapon : MonoBehaviour
    {
        public Animator animator;
        public bool aiWeapon = false;


        // this data should come from item system
        public string WeaponType = "Sword";
        public List<BaseStat> Stats { get; set; }

        public Damage damage { get; set; }
        public float AttackCoolDown { get ; set ; }

        // --------------------
        public Collider collider;
        public float WeaponRange;
        public bool FinishedAttack = false;


        bool canAttack = true;


        void Start()
        {

            
            collider = GetComponent<Collider>();
            collider.gameObject.SetActive(true);
            collider.isTrigger = true;
            collider.enabled = false;


            // temp values until inventory system is created
            damage = new Damage(new DamageType(), 5, 0);
            AttackCoolDown = 0;


        }

        

        public void PerformAttack(Damage dmg)
        {
            if (canAttack)
            {
                canAttack = false;
                FinishedAttack = false;
                damage = dmg;
                StartCoroutine(Attack());

            }
        }

        public void PerformSpecialAttack()
        {
            animator.SetTrigger("Special_Attack_" + WeaponType);
        }

        void OnTriggerEnter(Collider col)
        {
            if(!aiWeapon)
            { 
                EnemyHealth enemyHealth;
                if (enemyHealth = col.transform.GetComponent<EnemyHealth>())
                {
                    enemyHealth.TakeDamage(damage);
                }
            }
            else
            {
                PlayerHealth playerHealth;
                if (playerHealth = col.transform.GetComponent<PlayerHealth>())
                {
                    playerHealth.TakeDamage(damage);
                }
            }
        }

        IEnumerator AttackCooldown()
        {

            yield return new WaitForSeconds(AttackCoolDown);
            canAttack = true;
        }


        IEnumerator Attack()
        {

            animator.SetTrigger("Attack_" + WeaponType);
            // wait for animation to finish playing 
            do
            {
                yield return new WaitForEndOfFrame();
            } while (animator.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f);




            FinishedAttack = true;
            canAttack = false;
           
            StartCoroutine(AttackCooldown());

        }
    }

}
