using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

public class AiAttackPlayerState : AiState
{
    public Transform playerTransform;

    private AIAttackAction currentAttack;
    private float attackDelayTimer;
    private float attackRecoveryTimer;
    private bool attacking;
    private bool canAttack;
    AiAgent agent;

    public AiStateID GetId()
    {
        return AiStateID.AttackPlayer;
    }
    public void Enter(AiAgent agent)
    {
        attacking = false;
        canAttack = true;

        this.agent = agent;


        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        agent.navMeshAgent.isStopped = true;

        displayIndicator(agent);
    }

   


    public void Update(AiAgent agent)
    {
       /* if (attacking == false && canAttack == true)
        {
            
        }
        else if (attackDelayTimer >= 0.0f && attacking == true)
        {
            attackDelayTimer -= Time.deltaTime;
        }
        else if(attackDelayTimer <= 0.0f && attacking == true)
        {
            executeAttack(agent);
        }
        else if (attackRecoveryTimer >= 0.0f)
        {
            attackRecoveryTimer -= Time.deltaTime;
        }
        else if (attackRecoveryTimer <= 0.0f )
        {

            agent.damageCollider.SetActive(false);
            agent.stateMachine.ChangeState(AiStateID.ChasePlayer);
        }


        */

    }


    private void displayIndicator(AiAgent agent)
    {
        attacking = true;
        attackDelayTimer = agent.config.attackDelay;
        agent.animator.SetTrigger("Attack_Spear");
    }

    private void executeAttack(AiAgent agent)
    {
        attacking = false;
        canAttack = false;
        attackRecoveryTimer = agent.config.attackRecovery;
    }

 

  
    public void Exit(AiAgent agent)
    {
        agent.attackWarningMesh.SetActive(false);
        agent.damageCollider.SetActive(false);
    }

}
