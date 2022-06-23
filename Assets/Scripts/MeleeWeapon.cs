using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class MeleeWeapon : MonoBehaviour
    {
        public Animator animator;
        public LayerMask damageLayer;
        


        // this data should come from item system
        public string WeaponType = "Sword";
        public List<BaseStat> Stats { get; set; }

        public Damage damage { get; set; }
        public float AttackCoolDown { get ; set ; }

        // --------------------
        public Collider collider;
       
        public bool FinishedAttack = false;
       



        bool canAttack = true;
        private int comboCount = 0;

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
            bool canDoCombo = animator.GetBool("canDoCombo");

            if (canAttack|| canDoCombo)
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

            if (damageLayer == (damageLayer | ( 1 << col.gameObject.layer )))
            { 
                EnemyHealth enemyHealth;
                if (enemyHealth = col.transform.GetComponent<EnemyHealth>())
                {
                    enemyHealth.TakeDamage(damage);
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
        }


      

        IEnumerator AttackCooldown()
        {

            yield return new WaitForSeconds(AttackCoolDown);
            canAttack = true;
        }


        IEnumerator Attack()
        {


            animator.SetTrigger("Attack_" + WeaponType );
            // wait for animation to finish playing 
            int w = animator.GetCurrentAnimatorClipInfo(0).Length;
            string[] clipName = new string[w];
            for (int i = 0; i < w; i += 1)
            {
                clipName[i] = animator.GetCurrentAnimatorClipInfo(0)[i].clip.name;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_" + WeaponType)){

                yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            }
           

            canAttack = false;


            Debug.Log(animator.GetCurrentAnimatorStateInfo(1).normalizedTime);
            FinishedAttack = true;
            comboCount++;
           
            StartCoroutine(AttackCooldown());

        }
    }

}
