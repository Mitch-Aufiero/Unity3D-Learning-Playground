using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

public class AiMeleeWeapons : MonoBehaviour, AiWeapons
{
    public float damageAmount;
    public DamageType damageType;
    public float damageDelay;
    public LayerMask layers;
    public float attackStoppingDistance;
    public float attackRadius;
    public float attackCoolDown;
    public ParticleSystem attackParticle;


    Animator animator;
    bool canAttack = true;
    float attackRange;


    public float WeaponRange { get ; set ; } // only get needed publicly

    public float CoolDown { get; set ; } // not needed publicly
    public bool FinishedAttack { get; set ; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        WeaponRange = attackRange;
       
        WeaponRange = attackStoppingDistance;
        CoolDown = attackCoolDown;
        FinishedAttack = false;

    }

    public void StartAttack()
    {

        if (canAttack)
        {
            FinishedAttack = false;
            StartCoroutine(Attack());
        }

    }


    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(CoolDown);
        canAttack = true;
    }

    IEnumerator Attack()
    {


        //animator.SetBool("Attack", true);
        animator.SetTrigger("Attack");
        // wait for animation to finish playing 
        Debug.Log("Starting Animation");
        do
        {
            yield return new WaitForEndOfFrame();
        } while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        Debug.Log("Finish Animation");

        Collider[] hits = Physics.OverlapSphere(this.transform.position, attackRadius, layers);
        //config.attackParticle.Emit(1);
        foreach (Collider hit in hits)
        {
            PlayerHealth playerHealth;
            if (playerHealth = hit.transform.GetComponent<PlayerHealth>())
            {
                playerHealth.TakeDamage(new Damage(damageType, damageAmount, damageDelay));

            }
        }
        //animator.SetBool("Attack", false);
        FinishedAttack = true;
        StartCoroutine(AttackCooldown());
    }

  
}
