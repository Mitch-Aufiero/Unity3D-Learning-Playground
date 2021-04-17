using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class CharacterAiming : MonoBehaviour
{
    public float turnSpeed = 15;
    //public float aimDuration = .3f;
    Camera myCamera;



    // Start is called before the first frame update
    void Start()
    {
        myCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void AimCharacter(float delta)
    {
        float yawCamera = myCamera.transform.rotation.eulerAngles.y; // gets rotation of cameras Y axis
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * delta); //blends characters Y  rotation with the cameras Y

    }

}
