using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;


[CreateAssetMenu(menuName = "DungeonRPScripts/AI Agent Config")]
public class AiAgentConfig : ScriptableObject
{

    public float chaseResetTimer = 1.0f;
    public float attackStoppingDistance;
    public float attackCoolDown;
    public float attackDamage;

}
