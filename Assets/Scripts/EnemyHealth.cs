using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public GameObject healthBarUI;
    public Slider healthSlider;
    public DamageType[] VulnerableDamageTypes;
    public ParticleSystem vulnerableParticle;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }


    public void TakeDamage(float damageAmount, DamageType damageType , RaycastHit hitPosition)
    {
       
        if (damageType != null)
        {
            foreach(DamageType type in VulnerableDamageTypes)
            {
                if(type == damageType)
                {
                    Debug.Log("Vulnerable");
                    damageAmount *= 2;
                    vulnerableParticle.transform.position = hitPosition.point;
                    vulnerableParticle.transform.forward = hitPosition.normal;
                    vulnerableParticle.Emit(1);

                }
            }
        }


        
        

        health -= damageAmount;
        CalculateHealth();
    }

    void CalculateHealth()
    {

        if(health > maxHealth) // shields can increase max health or be a new value entirely
        {
            health = maxHealth;
        }

        if(health < 0)
        {
            KillEnemy();
        }

        if(health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }

        healthSlider.value = health / maxHealth;
    }

    void KillEnemy()// play death animation, drop items, give exp etc.
    {

        Destroy(gameObject);
    }
    
}
