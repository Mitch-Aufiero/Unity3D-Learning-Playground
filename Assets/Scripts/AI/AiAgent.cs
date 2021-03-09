using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;
    public AiStateID initialState;
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config;
   // public Ragdoll ragdoll;
    public SkinnedMeshRenderer mesh;
    //public UIHealthBar ui;

    // Start is called before the first frame update
    void Start()
    {
        //ragdoll = GetComponent<RagDoll>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        //ui = GetComponentInChildren<UIHealthBar>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiDeathState());
        stateMachine.ChangeState(initialState);
        
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
