using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

public class PlayerInputManager : MonoBehaviour
{

    public CharacterLocomotion LocomotionController;// not yet implemented
    public CharacterAiming AimController;

    public WeaponController weapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LocomotionController.SetMovementAxes(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) 
            AimController.AimCharacter(Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            weapon.Attack();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            LocomotionController.Roll();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            LocomotionController.Jump();
        }
    }
}
