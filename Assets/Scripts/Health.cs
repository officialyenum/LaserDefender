using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 100;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;

    LevelManager levelManager;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    public int GetHealth()
    {
        return health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
            //Generate Pickup
            int pickup = Random.Range(1, 20);// where health is 1 and power up is 5;
            if (pickup == 2)
            {
                SpawnHealth();
            }
            if (pickup == 3)
            {
                SpawnPowerUp();
            }
        }
        else
        {
            levelManager.LoadGameOver();
        }
        Destroy(gameObject);
    }

    private void SpawnHealth()
    {
        Shooter shooter = gameObject.GetComponent<Shooter>();
        GameObject healthPrefab = shooter.GetHealthPrefab();
        Instantiate(healthPrefab, transform.position, Quaternion.identity);
    }

    private void SpawnPowerUp()
    {
        Shooter shooter = gameObject.GetComponent<Shooter>();
        GameObject powerUpPrefab = shooter.GetPowerUpPrefab();
        Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
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
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            audioPlayer.PlayDamageClip();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }
}
