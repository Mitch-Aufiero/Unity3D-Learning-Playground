using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Combat;

public class AiAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;
    public AiStateID initialState;
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config;
    // public Ragdoll ragdoll;
    public SkinnedMeshRenderer mesh;
    //public UIHealthBar ui;
    public AiSensor sensor;
    public GameObject playerTransform;

    public MeleeWeapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        //ragdoll = GetComponent<RagDoll>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        //ui = GetComponentInChildren<UIHealthBar>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;
        sensor = GetComponent<AiSensor>();
        weapon = GetComponentInChildren<MeleeWeapon>();
        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiWanderState());
        stateMachine.RegisterState(new AiAttackPlayerState());
        stateMachine.RegisterState(new AiDeathState());
        stateMachine.ChangeState(initialState);

    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
