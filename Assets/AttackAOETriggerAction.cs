using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
public class AttackAOETriggerAction : MonoBehaviour
{
    public DamageType damageType;
    public float damageAmount;
    public float damageDelay;

    public void Start()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<PlayerHealth>().TakeDamage(new Damage(damageType,damageAmount, damageDelay));
    }
}
