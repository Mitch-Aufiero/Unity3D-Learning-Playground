using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class WeaponController : MonoBehaviour
    {

        public GameObject PlayerRightHand;
        public MeleeWeapon MainWeapon;



        // Start is called before the first frame update
        void Start()
        {

        }

        public void  Attack()
        {
            MainWeapon.PerformAttack(CalculateDamage());
        }

        

        private Damage CalculateDamage()
        {
            Damage dmg = new Damage(MainWeapon.damage.damageType, MainWeapon.damage.damageAmount, MainWeapon.damage.damageDelay);
           // add character stats per weaponprimary stat etc.
           // dmg.damageAmount += CalculateCrit(dmg.damageAmount);
            
            return dmg;
        }


        public void ActivateWeaponCollider()
        {
            MainWeapon.collider.enabled = true;
        }

        public void DisableWeaponCollider()
        {
            MainWeapon.collider.enabled = false;
        }


    }
}