using System.Collections;
using UnityEngine;

namespace Combat {
    public class MeleeAttackController : MonoBehaviour
    {
       

        Animator animator;


        public DamageType damageType;
        public float damageAmount;
        public float damageDelay;
        public float attackRadius = 10.0f;

        public float coolDown = 0.0f;
        bool canAttack = true;
        bool finishedAttack = false;
        float attackRange;

        
        private void Awake()
        {
            animator = GetComponent<Animator>();
            
            finishedAttack = false;

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)){
               StartAttack();
            }
        }



      

        public void StartAttack()
        {

            if (canAttack)
            {
                canAttack = false;
                finishedAttack = false;
                StartCoroutine(Attack());
            }

        }


        IEnumerator AttackCooldown()
        {
            
            yield return new WaitForSeconds(coolDown);
            canAttack = true;
        }

        IEnumerator Attack()
        {

            animator.SetTrigger("Attack");
            
            // wait for animation to finish playing 
            do
            {
                yield return new WaitForEndOfFrame();
            } while (animator.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f);

            Collider[] hits = Physics.OverlapSphere(this.transform.position, attackRadius); // move to animation event!!
           
            foreach (Collider hit in hits)
            {
                EnemyHealth enemyHealth;
                if (enemyHealth = hit.transform.GetComponent<EnemyHealth>())
                {
                    enemyHealth.TakeDamage(new Damage(damageType, damageAmount, damageDelay));

                }
            }
            
           
            finishedAttack = true;
            canAttack = false;
            StartCoroutine(AttackCooldown());

        }




    }

}
