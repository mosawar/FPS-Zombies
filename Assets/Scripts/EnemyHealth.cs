using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;

    private void Awake()
    {
        currentHealth = maxHealth;

        // Use GetComponentInChildren to find the Animator on a child object
        animator = GetComponentInChildren<Animator>();
    }

    // This method will be called when the enemy takes damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Check if the enemy's health is zero or less
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // This method will handle enemy death
    private void Die()
    {
        // Get the current animator state information
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Check if the enemy is walking and play the appropriate death animation
        if (stateInfo.IsName("Z_Walk_InPlace"))
        {
            // Trigger forward death
            animator.SetTrigger("DieForward");
        }
        else if (stateInfo.IsName("Z_Idle") || stateInfo.IsName("Z_Attack"))
        {
            // Trigger backward death
            animator.SetTrigger("DieBackward");
        }

        // Disable enemy behavior after death, such as AI or movement
        GetComponent<Collider>().enabled = false; // Prevent further collisions
        this.enabled = false; // Disable this script or other components
    }
}
