using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject[] projectilePrefabs;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [Header("Firing AI")]
    [SerializeField] float baseFiringRate = 0.2f;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;

    [SerializeField] bool useAI;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    UIDisplay uIDisplay;

    [HideInInspector] public bool isFiring;

    int firingNumber = 0;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        uIDisplay = FindObjectOfType<UIDisplay>();
    }
    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (isFiring)
        {
            GameObject projectileInstance = Instantiate(projectilePrefabs[firingNumber], transform.position, Quaternion.identity);
            Rigidbody2D projectileRB = projectileInstance.GetComponent<Rigidbody2D>();
            if (projectileRB != null )
            {
                projectileRB.velocity = transform.up * projectileSpeed;
            }
            Destroy(projectileInstance, projectileLifeTime);
            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);
            audioPlayer.PlayShootingClip();
            yield return new WaitForSeconds(timeToNextProjectile); 
        }
    }

    public void IncreaseFiringRate()
    {
        StartCoroutine(IncreaseFiringForTenSeconds());
    }


    IEnumerator IncreaseFiringForTenSeconds()
    {
        firingNumber = 1;
        baseFiringRate = 0.1f;
        uIDisplay.ShowRiot();
        yield return new WaitForSeconds(10f);
        firingNumber = 0;
        baseFiringRate = 0.2f;
        uIDisplay.RemoveRiot();
    }

    public GameObject GetHealthPrefab()
    {
        return projectilePrefabs[2];
    }

    public GameObject GetPowerUpPrefab()
    {
        return projectilePrefabs[3];
    }

    public GameObject GetShieldPrefab()
    {
        return projectilePrefabs[4];
    }
}
