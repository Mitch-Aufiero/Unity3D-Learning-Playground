using UnityEngine;

namespace Combat
{
    public class Damage 
    {
        public DamageType damageType;
        public float damageAmount;
        public float damageDelay;
        public RaycastHit hitPosition;

        public Damage(DamageType cDamageType, float cDamageAmount, float cDamageDelay)
        {
            this.damageType = cDamageType;
            this.damageAmount = cDamageAmount;
            this.damageDelay = cDamageDelay;
        }

    }
}