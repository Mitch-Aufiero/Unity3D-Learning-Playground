using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAttackPlayerState : AiState
{

    public AiStateID GetId()
    {
        return AiStateID.AttackPlayer;
    }
    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.stoppingDistance = agent.config.attackStoppingDistance;
        agent.navMeshAgent.speed = agent.config.attackSpeed;
    }
    public void Update(AiAgent agent)
    {
        UpdateAttacking(agent);
    }

    private void UpdateAttacking(AiAgent agent)
    {
        if (agent.sensor.IsInSight(agent.playerTransform.gameObject))
        {
           // agent.weapons.SetAttacking(true);
        }
        else
        {
           // agent.weapons.SetAttacking(false) ;
        }
    }


    public void Exit(AiAgent agent)
    {
         agent.navMeshAgent.stoppingDistance = 0.0f;
    }
}
