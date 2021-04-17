using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

public class AiAttackPlayerState : AiState
{
    public Transform playerTransform;
    float attackRange = 0.0f;
    float rotationSpeed =100f;


    public AiStateID GetId()
    {
        return AiStateID.AttackPlayer;
    }
    public void Enter(AiAgent agent)
    {
        
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        agent.navMeshAgent.isStopped = true;

        attackRange = agent.weapon.WeaponRange;
     

    }

    public void Update(AiAgent agent)
    {
       


        if (agent.navMeshAgent.remainingDistance > attackRange || agent.weapon.FinishedAttack == true) // maybe use stopping distance instead?
        {
            agent.navMeshAgent.destination = playerTransform.position;
            agent.stateMachine.ChangeState(AiStateID.ChasePlayer);
        }
        UpdateAttacking(agent);

    }

    private void UpdateAttacking(AiAgent agent)
    {
        //RotateTowards(agent, playerTransform);
        agent.weapon.PerformAttack(agent.weapon.damage);


    }




    public void Exit(AiAgent agent)
    {
    }

    private void RotateTowards(AiAgent agent,Transform target)
    {
        Vector3 direction = (target.position - agent.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
