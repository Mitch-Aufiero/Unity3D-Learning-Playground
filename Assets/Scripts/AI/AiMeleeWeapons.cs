using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

public class AiMeleeWeapons : MonoBehaviour, AiWeapons
{

    public LayerMask layers;
    public AiMeleeWeaponConfig config;

    Animator animator;
    bool canAttack = true;
    float attackRadius = 0.0f;
    Damage attackDamage;
    float attackRange;


    public float WeaponRange { get ; set ; } // only get needed publicly

    public float CoolDown { get; set ; } // not needed publicly
    public bool FinishedAttack { get; set ; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        WeaponRange = attackRange;
        attackRadius = config.attackRadius;
        attackDamage = config.attackDamage;
        WeaponRange = config.attackStoppingDistance;
        CoolDown = config.attackCoolDown;
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

        Collider[] hits = Physics.OverlapSphere(this.transform.position, attackRadius, layers);
        config.attackParticle.Emit(1);
        foreach (Collider hit in hits)
        {
            Debug.Log("hit: " + hit.name);
        }

        animator.SetBool("Attack", true);
        // wait for animation to finish playing 
        do
        {
            yield return new WaitForEndOfFrame();
        } while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);

        animator.SetBool("Attack", false);
        FinishedAttack = true;
        StartCoroutine(AttackCooldown());
    }

  
}
