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
        public ParticleSystem vulnerableParticle;
        public AiAgent agent;

        Camera cameraMain;

        Animator animator;
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


        public void TakeDamage(Damage damage)
        {
       
            if (damage.damageType != null)
            {
                foreach(DamageType type in VulnerableDamageTypes)
                {
                    if(type == damage.damageType)
                    {

                        damage.damageAmount *= 1.5f;
                        vulnerableParticle.transform.position = damage.hitPosition.point;
                        vulnerableParticle.transform.forward = damage.hitPosition.normal;
                        vulnerableParticle.Emit(1);

                    }
                }
            }

            health -= damage.damageAmount;
            CalculateHealth();
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