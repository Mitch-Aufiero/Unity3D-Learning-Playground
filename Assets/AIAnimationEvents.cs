using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationEvents : MonoBehaviour
{

    public GameObject damageIndicator;
    public GameObject damageCollider;
    public AiAgent agent;

    public void Start()
    {
       // this.agent = GetComponent<AiAgent>();
    }

    public void ActivateDamageIndicator()
    {

        damageIndicator.SetActive(true);
    }

    public void ActivateDamageArea()
    {

        damageIndicator.SetActive(false);
        damageCollider.SetActive(true);
    }

    public void DisableDamageArea()
    {

        damageCollider.SetActive(false);
    }

    public void ExitAttackAnimation()
    {

        agent.stateMachine.ChangeState(AiStateID.ChasePlayer);
    }
}
