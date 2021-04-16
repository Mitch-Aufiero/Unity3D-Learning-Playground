using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChasePlayerState : AiState
{
    public Transform playerTransform;
    float timer = 0.0f;

    public AiStateID GetId()
    {
        return AiStateID.ChasePlayer;
    }
    public void Enter(AiAgent agent)
    {
        
        if(playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        agent.navMeshAgent.isStopped = false;
    }
    public void Update(AiAgent agent)
    {
        if (!agent.enabled)
        {
            return;
        }

        timer -= Time.deltaTime;
        if (!agent.navMeshAgent.hasPath)
        {
            agent.navMeshAgent.destination = playerTransform.position;
        }

        if (timer < 0.0f)
        {
            Vector3 direction = (playerTransform.position - agent.navMeshAgent.destination);
            direction.y = 0;
            //if(direction.sqrMagnitude > agent.config.maxDistance * agent.config.maxDistance)
            {
              //  if(agent.navMeshAgent.pathStatus != UnityEngine.AI.NavMeshPathStatus.PathPartial)
                {
                    agent.navMeshAgent.destination = playerTransform.position;
                }
            }
            timer = agent.config.maxTime;
        }

        if (agent.navMeshAgent.remainingDistance < agent.weapon.WeaponRange)
        {

            agent.stateMachine.ChangeState(AiStateID.AttackPlayer);
        }
    }

    public void Exit(AiAgent agent)
    {
        
    }
}
