using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    public event Action onDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
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

        
        GetComponent<Collider>().enabled = false; // Prevent further collisions
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false; // disable navmesh movement
        this.enabled = false; // stop AI logic

    //call destroy enemy 
    StartCoroutine(DestroyAfterDeath());
}

    private IEnumerator DestroyAfterDeath()
    {
        // wait til animation finishes
        while (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Death") || animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        onDeath?.Invoke();
        // destroy enemy
        Destroy(gameObject);
    }
}
