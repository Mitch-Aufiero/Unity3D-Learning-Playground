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
    public float attackDelay = 1.5f;
    public float attackRecovery = .5f;
    public AnimationClip attackAnimation;

    public float attackConeAngle = 30;
    public float attackConeDistance = 3;
    public float attackConeHeight = 1.0f;

    public LayerMask occlusionLayers;

    public AIAttackAction[] AiAttacks;

}
