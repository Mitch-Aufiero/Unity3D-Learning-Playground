using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

public class PlayerInputManager : MonoBehaviour
{

    public CharacterLocomotion loc;// not yet implemented

    public WeaponController weapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            weapon.Attack();
        }
    }
}
