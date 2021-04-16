using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiWanderState : AiState
{
    public List<Transform> playerTransforms;

    public AiStateID GetId()
    {
        return AiStateID.Wander;
    }
    public void Enter(AiAgent agent)
    {
        if (playerTransforms == null)
        {
            playerTransforms = new List<Transform>();
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
            {
                playerTransforms.Add(obj.GetComponent<Transform>());
            }
        }
    }
    public void Update(AiAgent agent)
    {
        if (!agent.enabled)
        {
            return;
        }
        if (!agent.navMeshAgent.hasPath)
        {
            WorldBounds worldBounds = GameObject.FindObjectOfType<WorldBounds>();
            Vector3 min = worldBounds.min.position;
            Vector3 max = worldBounds.max.position;

            Vector3 randomPosition = new Vector3(
                Random.Range(min.x, max.x),
                Random.Range(min.y, max.y),
                Random.Range(min.z, max.z));

            agent.navMeshAgent.destination = randomPosition;
        }

        foreach(Transform playerTransform in playerTransforms)
        { 
            if (agent.sensor.IsInSight(playerTransform.gameObject))
            {
                
                agent.stateMachine.ChangeState(AiStateID.ChasePlayer);
            }
        }
    }

    public void Exit(AiAgent agent)
    {

    }
}
