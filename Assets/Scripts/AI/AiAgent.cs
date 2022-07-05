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
    public EnemyHealth health;
    public AiSensor sensor;
    public AiAttackSensor attackSensor;
    public GameObject playerTransform;
    public GameObject attackWarningMesh;
    public GameObject damageCollider;
    public Animator animator;


    public MeleeWeapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        health = GetComponentInChildren<EnemyHealth>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;
        sensor = GetComponent<AiSensor>();
        attackSensor = GetComponent<AiAttackSensor>();

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
