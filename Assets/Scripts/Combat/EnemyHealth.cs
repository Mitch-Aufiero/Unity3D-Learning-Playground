using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Combat { 
    public class EnemyHealth : MonoBehaviour, Damagable
    {
        public float health;
        public float maxHealth;

        public GameObject healthBarUI;
        public Slider healthSlider;
        public DamageType[] VulnerableDamageTypes;
        public ParticleSystem hitParticle;
        public AiAgent agent;
        public AudioSource enemyAudioSource;

        Camera cameraMain;

        public Animator animator;
        Collider collider;



        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<AiAgent>();
            collider = GetComponent<Collider>();

            animator = GetComponent<Animator>();

            health = maxHealth;
            cameraMain = Camera.main;
        }


        public bool TakeDamage(Damage damage)
        {
            bool wasVulnerable = false;
            float damageAmount = damage.damageAmount;

            enemyAudioSource.clip = damage.hitAudio;

            if (damage.damageType != null)
            {
                foreach(DamageType type in VulnerableDamageTypes)
                {
                    if(type == damage.damageType)
                    {
                        wasVulnerable = true;
                        


                        damageAmount *= 1.5f;

                    }
                }
            }

            animator.SetTrigger("Damaged");

            enemyAudioSource.Play();
            health -= damageAmount;
            CalculateHealth();
            return wasVulnerable;

        }

      

        void CalculateHealth()
        {

            if(health > maxHealth) // shields can increase max health or be a new value entirely
            {
                health = maxHealth;
            }

            if(health <= 0)
            {
                healthBarUI.SetActive(false);
                animator.SetTrigger("Death");
                agent.stateMachine.ChangeState(AiStateID.Death);
                collider.enabled = false;
                StartCoroutine(Die());
            }
            else if (health < maxHealth)
            {
                healthBarUI.SetActive(true);
            }


            healthSlider.value = health / maxHealth;
        }

   


        IEnumerator Die()
        {

            yield return new WaitForSeconds(6);
            Destroy(gameObject);
           
        }

        private void LateUpdate()// look into less taxing way of updating canvas. InvokeRepeating?
        {

                healthBarUI.transform.LookAt(cameraMain.transform.position + cameraMain.transform.rotation * Vector3.back, cameraMain.transform.rotation * Vector3.up);

        }
    }

}