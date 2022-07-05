using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{

    public interface Damagable
    {

        public bool TakeDamage(Damage damage);
    
    }

}