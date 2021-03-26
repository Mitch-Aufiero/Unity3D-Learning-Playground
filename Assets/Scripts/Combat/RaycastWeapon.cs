using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{

    public class RaycastWeapon : MonoBehaviour
    {
        class Bullet
        {
            public float time;
            public Vector3 initPosition;
            public Vector3 initVelocity;
            public int bounce;
            public TrailRenderer tracer;
        }

        public ActiveWeapon.WeaponSlot weaponSlot;
        public bool isFiring = false;
        public int fireRate = 25;
        public int ammoCount = 6;
        public int clipSize = 6;
        public float damageAmount;
        public DamageType damageType;
        public float bulletSpeed = 1000;
        public float bulletDrop = 0.0f;
        public int maxBounces = 0;
        public int impulseForce = 10;
        public ParticleSystem muzzleFlash;
        public ParticleSystem hitEffect;
        public TrailRenderer tracerEffect;
        public AnimationClip weaponEquipAnimation;
        public Transform raycastOrigin;
        public Transform raycastDestination;
        public WeaponRecoil recoil;
        public GameObject magazine;
        public bool reloading = false;

        

        public string weaponAnimName;


        Ray ray;
        RaycastHit hitInfo;
        float accumulatedTime;
        List<Bullet> bullets = new List<Bullet>();
        float bulletsMaxLifeTime = 1.0f;


        private void Awake()
        {
            recoil = GetComponent<WeaponRecoil>();
        }


        private void FireBullet()
        {
            if(ammoCount<= 0)
            {
                return;
            }
            ammoCount--;
            muzzleFlash.Emit(1);

            Vector3 velocity = (raycastDestination.position - raycastOrigin.position).normalized * bulletSpeed;
            var bullet = CreateBullet(raycastOrigin.position, velocity);
            bullets.Add(bullet);

            recoil.GenerateRecoil(weaponAnimName);

        }

        public void UpdateWeapon(float deltaTime)
        {
            if (!reloading)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    StartFiring();
                }
                if (isFiring)
                {
                    UpdateFiring(deltaTime);
                }

            }
            UpdateBullets(deltaTime);
            if (Input.GetButtonUp("Fire1") || reloading)
            {
                StopFiring();
            }
        }



        Vector3 GetPosition(Bullet bullet)
        {
            Vector3 gravity = Vector3.down * bulletDrop;
            return bullet.initPosition + bullet.initVelocity * bullet.time + .5f * gravity * bullet.time * bullet.time;
        }

        Bullet CreateBullet(Vector3 position, Vector3 velocity)
        {
            Bullet bullet = new Bullet();
            bullet.initPosition = position;
            bullet.initVelocity = velocity;
            bullet.time = 0.0f;
            bullet.tracer = Instantiate(tracerEffect, position, Quaternion.identity);
            bullet.tracer.AddPosition(position);
            bullet.bounce = maxBounces;
            return bullet;
        }

        public void UpdateFiring(float deltaTime)
        {
            accumulatedTime += deltaTime;

            float fireInterval = 1.0f / fireRate;


            while (accumulatedTime >= 0.0f)
            {
                FireBullet();
                accumulatedTime -= fireInterval;



            }

        }

        public void StartFiring()
        {
            isFiring = true;
            accumulatedTime = 0.0f;
            recoil.Reset();

        }

        public void UpdateBullets(float deltaTime)
        {
            SimulateBullets(deltaTime);
            DestroyBullets();
        }

        void SimulateBullets(float deltaTime)
        {
            bullets.ForEach(bullet =>
           {
               Vector3 p0 = GetPosition(bullet);
               bullet.time += deltaTime;
               Vector3 p1 = GetPosition(bullet);
               RaycastSegment(p0, p1, bullet);
           });
        }

        void DestroyBullets()
        {
            bullets.RemoveAll(bullet => bullet.time >= bulletsMaxLifeTime);
        }

        void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
        {
            Vector3 direction = end - start;
            float distance = direction.magnitude;
            ray.origin = start;
            ray.direction = direction;
            if (Physics.Raycast(ray, out hitInfo, distance))
            {

                hitEffect.transform.position = hitInfo.point;
                hitEffect.transform.forward = hitInfo.normal;
                bullet.time = bulletsMaxLifeTime;


                bullet.tracer.transform.position = hitInfo.point;

                hitEffect.Emit(1);

                EnemyHealth enemyHealth;
                if (enemyHealth = hitInfo.transform.GetComponent<EnemyHealth>())
                {
                    enemyHealth.TakeDamage(new Damage( damageType, damageAmount, 0.0f));

                }


                // bullet ricochet
                if (bullet.bounce > 0)
                {
                    Debug.Log(bullet.bounce);
                    bullet.time = 0;
                    bullet.initPosition = hitInfo.point;
                    bullet.initVelocity = Vector3.Reflect(bullet.initVelocity, hitInfo.normal);
                    bullet.bounce--;
                }

                //collision impulse
                var rb2d = hitInfo.collider.GetComponent<Rigidbody>();
                if (rb2d)
                {
                    rb2d.AddForceAtPosition(ray.direction * impulseForce, hitInfo.point, ForceMode.Impulse);
                }



            }
            else
            {
                if (bullet.tracer)
                    bullet.tracer.transform.position = end;
            }
        }


        public void StopFiring()
        {
            isFiring = false;
        }
    }

}