using UnityEngine;

namespace Combat
{
    
    [CreateAssetMenu(menuName = "DungeonRPScripts/Damage")]
    public class Damage : ScriptableObject
    {
        public DamageType damageType;
        public float damageAmount;
        public float damageDelay;
        public RaycastHit hitPosition;
        public AudioClip hitAudio;
        public AudioClip vulnerableHitAudio;



    }
}