using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDeathState : AiState
{

    public Vector3 direction;
    public void Enter(AiAgent agent)
    {

        
        agent.navMeshAgent.enabled = false;
        
        
    }




    public void Exit(AiAgent agent)
    {
        //drop items award exp
    }

    public AiStateID GetId()
    {
        return AiStateID.Death;
    }

    public void Update(AiAgent agent)
    {
        
    }

}
