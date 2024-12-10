using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour
{
    NavMeshAgent nm;
    public Transform target; // Drag the player object here in the Unity Editor
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



        // Check if the target has been assigned in the Inspector
        if (target == null)
        {
            // Find the player object by tag
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            target = player.transform;
            return;
        }

        aiState = AIState.idle; // Ensure the AI starts in idle
        StartCoroutine(Think());
    }

    IEnumerator Think()
    {
        while (true)
        {
            if (nm == null || !nm.isOnNavMesh)
            {
                yield break; // Exit the coroutine if the NavMeshAgent is invalid
            }

            switch (aiState)
            {
                case AIState.idle:
                    float dist = Vector3.Distance(target.position, transform.position);
                    if (dist < distanceThreshold)
                    {
                        aiState = AIState.chasing;
                        animator.SetBool("Chasing", true);
                    }
                    nm.SetDestination(transform.position);
                    break;

                case AIState.chasing:
                    dist = Vector3.Distance(target.position, transform.position);

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
                        nm.SetDestination(target.position);
                    }
                    break;

                case AIState.attack:
                    nm.SetDestination(transform.position);

                    dist = Vector3.Distance(target.position, transform.position);
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
                    break;

                default:
                    break;
            }

            yield return new WaitForSeconds(0.2f);
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
