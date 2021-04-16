using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat {
    public interface IWeapon 
    {

        List<BaseStat> Stats { get; set; }
        Damage damage { get; set; }
        float AttackCoolDown { get; set; }
        void PerformAttack(Damage dmg);
        void PerformSpecialAttack();
    }
}