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

    public AiStateID GetId()
    {
        return AiStateID.AttackPlayer;
    }
    public void Enter(AiAgent agent)
    {
        attacking = false;
        canAttack = true;


        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        agent.navMeshAgent.isStopped = true;
     

    }

   


    public void Update(AiAgent agent)
    {
        if (attacking == false && canAttack == true)
        {
            agent.attackWarningMesh.SetActive(true);
            attacking = true;
            attackDelayTimer = agent.config.attackDelay;
        }
        else if (attackDelayTimer >= 0.0f && attacking == true)
        {
            attackDelayTimer -= Time.deltaTime;
        }
        else if(attackDelayTimer <= 0.0f && attacking == true)
        {
            agent.attackWarningMesh.SetActive(false);
            attacking = false;
            canAttack = false;
            attackRecoveryTimer = agent.config.attackRecovery;
        }
        else if (attackRecoveryTimer >= 0.0f)
        {
            attackRecoveryTimer -= Time.deltaTime;
        }
        else if (attackRecoveryTimer <= 0.0f )
        {
            agent.stateMachine.ChangeState(AiStateID.ChasePlayer);
        }




    }

  
    public void Exit(AiAgent agent)
    {
        agent.attackWarningMesh.SetActive(false);
    }

}
