using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] ParticleSystem hitEffect;

    public int GetHealth()
    {
        return health;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Took " + damage + " damage");
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(int heal)
    {
        health += heal;
    }

    void PlayHitEffect()
    {
        if (hitEffect)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with " + other.name);
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            damageDealer.Hit();
        }
    }
}
