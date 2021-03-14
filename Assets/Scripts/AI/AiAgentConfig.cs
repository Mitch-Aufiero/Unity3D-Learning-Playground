using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;


[CreateAssetMenu(menuName = "DungeonRPScripts/AI Agent Config")]
public class AiAgentConfig : ScriptableObject
{

    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    public float dieForce = 10.0f;
    public float attackStoppingDistance;
    public float attackSpeed;
    public float attackRange;
    public float attackCoolDown;
    public Damage attackDamage;
}
