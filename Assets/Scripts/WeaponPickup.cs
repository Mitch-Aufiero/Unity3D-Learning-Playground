using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public RaycastWeapon weaponPrefab;
    public WeaponGenerator generator;

    private void OnTriggerEnter(Collider other)
    {
        ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();
        if (activeWeapon)
        {

            RaycastWeapon newWeapon;
            if (weaponPrefab)
            {
                newWeapon = Instantiate(weaponPrefab);
            }
            else
            {
                newWeapon =generator.GenerateWeapon();
            }

            activeWeapon.Equip(newWeapon);
        }
    }
}
