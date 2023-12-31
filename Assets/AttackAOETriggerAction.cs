using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
public class AttackAOETriggerAction : MonoBehaviour
{
    public DamageType damageType;
    public float damageAmount;
    public float damageDelay;
    public Damage damage;

    public void Start()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        PlayerHealth health;
        if (health = other.gameObject.GetComponent<PlayerHealth>())
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
    }
}
