﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class CharacterAiming : MonoBehaviour
{
    public float turnSpeed = 15;
    public float aimDuration = .3f;
    public Rig aimLayer;
    Camera myCamera;

    RaycastWeapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        weapon = GetComponentInChildren<RaycastWeapon>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float yawCamera = myCamera.transform.rotation.eulerAngles.y; // gets rotation of cameras Y axis

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.deltaTime); //blends characters Y  rotation with the cameras Y
        
    }

    private void LateUpdate()
    {
        if (aimLayer)
        {
            if (Input.GetButton("Fire2"))
            {
                aimLayer.weight += Time.deltaTime / aimDuration;
            }
            else
            {
                aimLayer.weight -= Time.deltaTime / aimDuration;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            weapon.StartFiring();
        }
        if (weapon.isFiring)
        {
            weapon.UpdateFiring(Time.deltaTime);
        }
        weapon.UpdateBullets(Time.deltaTime);
        if (Input.GetButtonUp("Fire1"))
        {
            weapon.StopFiring();
        }
        
    }
}
