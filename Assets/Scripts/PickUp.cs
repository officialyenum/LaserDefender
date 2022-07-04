using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] bool isHealth = true;
    [SerializeField] bool isPowerUp = false;
    // Start is called before the first frame update
    Player player;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player Success");
            //Process PickUp
            ProcessPickUp();
            Destroy(gameObject);
        }
    }


    private void ProcessPickUp()
    {
        if(isHealth)
        {
            AddHealth();
        }
        if(isPowerUp)
        {
            IncreaseBaseRate();
        }
    }

    private void AddHealth()
    {
        player.GetComponent<Health>().Heal(10);
    }

    private void IncreaseBaseRate()
    {
        player.GetComponent<Shooter>().IncreaseFiringRate();
    }

}
