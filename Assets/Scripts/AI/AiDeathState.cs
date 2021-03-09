using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDeathState : AiState
{

    public Vector3 direction;
    public void Enter(AiAgent agent)
    {
       // agent.ragdoll.ActivateRagdoll();
        //direction.y = 1;
        //agent.ragdoll.ApplyForce(direction * agent.config.dieForce);
        //agent.ui.gameObject.SetActive(false);
        //agent.mesh.updateWhenOffScreen = true;
    }

    public void Exit(AiAgent agent)
    {
        throw new System.NotImplementedException();
    }

    public AiStateID GetId()
    {
        return AiStateID.Death;
    }

    public void Update(AiAgent agent)
    {
        throw new System.NotImplementedException();
    }

}
