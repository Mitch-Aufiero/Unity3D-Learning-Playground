using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiLocomotion : MonoBehaviour
{

    public Transform target;
    public string MovementType;
    NavMeshAgent agent;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if(MovementType != "")
            ChangeMovementType(MovementType);

        agent.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.hasPath) { 
            animator.SetFloat("Speed", agent.velocity.magnitude);
        } else
        {
            animator.SetFloat("Speed", 0);
        }
    }

    public void ChangeMovementType(string movementType)
    {
        animator.SetBool(movementType, true);
    }
}
