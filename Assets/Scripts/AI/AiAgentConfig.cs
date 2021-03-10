using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "DungeonRPScripts/AI Agent Config")]
public class AiAgentConfig : ScriptableObject
{

    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    public float dieForce = 10.0f;
    internal float attackStoppingDistance;
    internal float attackSpeed;
}
