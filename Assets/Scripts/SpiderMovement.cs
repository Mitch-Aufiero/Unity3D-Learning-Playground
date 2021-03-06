using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderMovement : MonoBehaviour
{
    // if leg is ready to move(idk how to tell this)
    // start coroutine of removing leg rig weight once done set target to new position(cast ray) set weight back to 1

    public Transform target;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        agent.SetDestination(target.position);

    }
        

}
