using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChasePlayerState : AiState
{
    public List<Transform> playerTransforms;
    public Transform targetPlayerTransform;
    float timer = 0.0f;

    public AiStateID GetId()
    {
        return AiStateID.ChasePlayer;
    }
    public void Enter(AiAgent agent)
    {

        if (targetPlayerTransform == null)
        {
            targetPlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }



        if (playerTransforms == null)
        {
            playerTransforms = new List<Transform>();
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
            {

                playerTransforms.Add(obj.GetComponent<Transform>());
            }
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
            agent.navMeshAgent.destination = targetPlayerTransform.position;
        }

        if (timer < 0.0f)
        {
            Vector3 direction = (targetPlayerTransform.position - agent.navMeshAgent.destination);
            direction.y = 0;
            //if(direction.sqrMagnitude > agent.config.maxDistance * agent.config.maxDistance)
            {
              //  if(agent.navMeshAgent.pathStatus != UnityEngine.AI.NavMeshPathStatus.PathPartial)
                {
                    agent.navMeshAgent.destination = targetPlayerTransform.position;
                }
            }
            timer = agent.config.chaseResetTimer;
        }


        foreach (Transform playerTransform in playerTransforms)
        {
            if (agent.attackSensor.IsInRangeOf(playerTransform.gameObject))
            {
                agent.stateMachine.ChangeState(AiStateID.AttackPlayer);
            }
        }

    }

    public void Exit(AiAgent agent)
    {
        
    }
}
