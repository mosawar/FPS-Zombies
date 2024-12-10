using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Damage dealt by this bullet
    private int damage;

    // Method to set damage value
    public void SetDamage(int weaponDamage)
    {
        damage = weaponDamage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            // Get the EnemyHealth script from the enemy
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            // Destroy the bullet
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            // print("hit a Wall");
            Destroy(gameObject);
        }
    }
}