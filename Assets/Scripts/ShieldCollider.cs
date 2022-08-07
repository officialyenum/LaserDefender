using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollider : MonoBehaviour
{
    Shield shield;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            Destroy(collision.gameObject);
            Invoke("DestroyShield", 2f);
        }
    }

    void DestroyShield()
    {
        shield = FindObjectOfType<Shield>();
        if (shield.shieldOn)
        {
            if (shield.shieldHealth < 0)
            {
                shield.TurnOffShield();
            }
            else
            {
                shield.shieldHealth--;
            }
        }
    }
}
