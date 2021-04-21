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

    private AIAttackAction currentAttack;


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

        attackRange = agent.config.attackStoppingDistance;
     

    }

    private void GetNewAttack(AiAgent agent)
    {
        Vector3 targetsDirection = playerTransform.position - agent.transform.position;
        float viewableAngle = Vector3.Angle(targetsDirection, agent.transform.forward);


        int maxScore = 0;

        for (int i = 0; i < agent.config.AiAttacks.Length; i++)
        {
            AIAttackAction aiAttack = agent.config.AiAttacks[i];
            if(agent.navMeshAgent.remainingDistance <= aiAttack.maximumDistanceNeededToAttack
                && agent.navMeshAgent.remainingDistance >= aiAttack.minimumDistanceNeededToAttack)
            {
                if(viewableAngle <= aiAttack.maximumAttackAngle 
                    && viewableAngle <= aiAttack.maximumAttackAngle)
                {
                    maxScore += aiAttack.attackScore;
                }
            }
        }


        int randomValue = UnityEngine.Random.Range(0, maxScore);
        int tempScore = 0;

        for (int i = 0; i < agent.config.AiAttacks.Length; i++)
        {
            AIAttackAction aiAttack = agent.config.AiAttacks[i];
            if (agent.navMeshAgent.remainingDistance <= aiAttack.maximumDistanceNeededToAttack
                && agent.navMeshAgent.remainingDistance >= aiAttack.minimumDistanceNeededToAttack)
            {
                if (viewableAngle <= aiAttack.maximumAttackAngle
                    && viewableAngle <= aiAttack.maximumAttackAngle)
                {
                    if (currentAttack != null)
                        return;
                    tempScore += aiAttack.attackScore;

                    if(tempScore > randomValue)
                    {
                        currentAttack = aiAttack;
                    }
                }
            }
        }

    }

    private void AttackTarget(AiAgent agent)
    {
        if(currentAttack == null)
        {
            GetNewAttack(agent);
        }

    }

    public void Update(AiAgent agent)
    {
       


        if (agent.navMeshAgent.remainingDistance > attackRange || agent.weapon.FinishedAttack == true) 
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
