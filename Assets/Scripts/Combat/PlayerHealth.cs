using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{


    public class PlayerHealth : MonoBehaviour, Damagable
    {

        public float health;
        public float maxHealth;

        public GameObject healthBarUI;
        public Slider healthSlider;
        public DamageType[] VulnerableDamageTypes;
        public ParticleSystem vulnerableParticle;
        Camera cameraMain;

        bool invulnerable;


        // Start is called before the first frame update
        void Start()
        {
            health = maxHealth;
            cameraMain = Camera.main;
            invulnerable = false;
        }


        public void TakeDamage(Damage damage ) // do math related to how much damage player should take
        {
            if (!invulnerable) { 
                health -= damage.damageAmount;
                CalculateHealth();
            }
        }

        void CalculateHealth()
        {

            if (health > maxHealth) // shields can increase max health or be a new value entirely
            {
                health = maxHealth;
            }

            if (health < 0)
            {
                Die();
            }

            if (health < maxHealth)
            {
                healthBarUI.SetActive(true);
            }

            healthSlider.value = health / maxHealth;
        }


        public void SetInvulnerable()
        {
            invulnerable = true;
        }
        public void SetVulnerable()
        {
            invulnerable = false;
        }


        void Die() 
        {

            Destroy(gameObject);
        }



    }


}