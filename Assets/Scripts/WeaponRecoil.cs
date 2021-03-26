using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
       public Cinemachine.CinemachineFreeLook playerCamera;
       public Cinemachine.CinemachineImpulseSource cameraShake;
       public Animator rigController;

    public Vector2[] recoilPattern;
    public float recoilDuration;


    float verticalRecoil;
    float horizontalRecoil;
    float time;
    int index;

    private void Awake()
    {
        cameraShake = GetComponent<CinemachineImpulseSource>();
    }

    public void Reset()
    {
        index = 0;
    }

    int NextIndex(int index)
    {
        return (index + 1) % recoilPattern.Length;
    }

    public void GenerateRecoil(string weaponName)
    {

        time = recoilDuration;
        cameraShake.GenerateImpulse(Camera.main.transform.forward);

        horizontalRecoil = recoilPattern[index].x;
        verticalRecoil = recoilPattern[index].y;

        index = NextIndex(index);

        rigController.Play("weapon_recoil_" + weaponName, 1, 0.0f);

    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            playerCamera.m_YAxis.Value -= ((verticalRecoil/1000) * Time.deltaTime) / recoilDuration;
            playerCamera.m_XAxis.Value -= ((horizontalRecoil/10) * Time.deltaTime) / recoilDuration;
            time -= Time.deltaTime;
        }
        
    }
}
