using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour
{
    private NavMeshAgent nm;
    public Transform target; // Automatically find the player object
    public float distanceThreshold = 50f;
    public float attackThreshold = 1.5f;
    public float attackCooldown = 1.5f;
    private float lastAttackTime = 0f;

    public enum AIState { idle, chasing, attack };
    public AIState aiState = AIState.idle;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        nm = GetComponent<NavMeshAgent>();

        // Try to find the player object
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform; // Cache the player's transform
        }
        else
        {
            Debug.LogError("Player object not found. Ensure the Player is tagged 'Player' and present in the scene.");
        }

        // Ensure the AI starts in idle state
        aiState = AIState.idle;

        // Start the Think coroutine
        if (nm != null && target != null)
        {
            StartCoroutine(Think());
        }
    }

    IEnumerator Think()
    {
        while (true)
        {
            // Ensure the NavMeshAgent is valid and on the NavMesh
            if (nm == null || !nm.isOnNavMesh)
            {
                yield break; // Exit the coroutine if invalid
            }

            switch (aiState)
            {
                case AIState.idle:
                    HandleIdleState();
                    break;

                case AIState.chasing:
                    HandleChasingState();
                    break;

                case AIState.attack:
                    HandleAttackState();
                    break;
            }

            yield return new WaitForSeconds(0.2f); // Small delay for better performance
        }
    }

    void HandleIdleState()
    {
        float dist = Vector3.Distance(target.position, transform.position);

        if (dist < distanceThreshold)
        {
            aiState = AIState.chasing;
            animator.SetBool("Chasing", true);
        }
        else
        {
            nm.SetDestination(transform.position); // Stay in place
        }
    }

    void HandleChasingState()
    {
        float dist = Vector3.Distance(target.position, transform.position);

        if (dist > distanceThreshold)
        {
            aiState = AIState.idle;
            animator.SetBool("Chasing", false);
        }
        else if (dist < attackThreshold)
        {
            aiState = AIState.attack;
            animator.SetBool("Attacking", true);
        }
        else
        {
            nm.SetDestination(target.position); // Move toward the player
        }
    }

    void HandleAttackState()
    {
        nm.SetDestination(transform.position); // Stop moving

        float dist = Vector3.Distance(target.position, transform.position);

        if (dist > attackThreshold)
        {
            aiState = AIState.chasing;
            animator.SetBool("Attacking", false);
        }
        else
        {
            if (Time.time > lastAttackTime + attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time;
            }
        }
    }

    void AttackPlayer()
    {
        Debug.Log("Zombie attacks the player!");

        // Check if the player has a PlayerHealth component
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(34); // Damage amount
        }
    }
}
