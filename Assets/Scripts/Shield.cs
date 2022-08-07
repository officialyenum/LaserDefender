using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] GameObject shieldPrefab;
    GameObject projectileInstance;
    UIDisplay uiDisplay;
    public bool shieldOn;
    public int shieldHealth = 2;
    private void Awake()
    {
        shieldOn = false;
    }

    private void Start()
    {
        uiDisplay = FindObjectOfType<UIDisplay>();
    }
    public void ActivateShield()
    {
        StartCoroutine(ActivateShieldForTenSeconds());
    }

    public int GetShieldHealth()
    {
        return shieldHealth;
    }

    IEnumerator ActivateShieldForTenSeconds()
    {
        if (!shieldOn)
        {
            TurnOnShield();
            yield return new WaitForSeconds(15f);
            TurnOffShield();
        }
    }

    void TurnOnShield()
    {
        InstantiateShieldToParent();
        uiDisplay.ShowShield();
        shieldOn = true;
    }

    public void TurnOffShield()
    {
        uiDisplay.RemoveShield();
        shieldOn = false;
        Destroy(projectileInstance);
    }

    private void InstantiateShieldToParent()
    {
        projectileInstance = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        projectileInstance.transform.parent = gameObject.transform;

    }


}
