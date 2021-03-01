using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DungeonRPScripts/Weapon Generator")]
public class WeaponGenerator : ScriptableObject
{

    public RaycastWeapon[] weaponBases;
    public DamageType[] damageTypes;
    public int weaponLevel;



    public RaycastWeapon GenerateWeapon()
    {
        RaycastWeapon generatedWeapon;

        int weaponBaseIndex = Random.Range(0, weaponBases.Length);
        int damageTypeIndex = Random.Range(0, damageTypes.Length);
        generatedWeapon = Instantiate(weaponBases[weaponBaseIndex]);

        generatedWeapon.damageAmount = (generatedWeapon.damageAmount * weaponLevel) + Random.Range(-weaponLevel, weaponLevel);

        generatedWeapon.damageType = damageTypes[damageTypeIndex];

        bool hasBounce = Random.Range(0, 1) > 0;
        if (hasBounce)
        {
            generatedWeapon.maxBounces = 3;
        }

        generatedWeapon.impulseForce = 10;

        return generatedWeapon;
    }
}
