using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public GameObject healthBarUI;
    public Slider healthSlider;
    public DamageType[] VulnerableDamageTypes;
    public ParticleSystem vulnerableParticle;
    Camera cameraMain;



    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        cameraMain = Camera.main;
    }


    public void TakeDamage(float damageAmount, DamageType damageType, RaycastHit hitPosition)
    {

        if (damageType != null)
        {
            foreach (DamageType type in VulnerableDamageTypes)
            {
                if (type == damageType)
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

        if (health > maxHealth) // shields can increase max health or be a new value entirely
        {
            health = maxHealth;
        }

        if (health < 0)
        {
            Die();
        }

        healthSlider.value = health / maxHealth;
    }

    void Die()// play death animation, show game over canvas
    {

        Destroy(gameObject);
    }


}
