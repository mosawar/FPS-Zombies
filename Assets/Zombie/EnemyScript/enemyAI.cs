using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour
{
    NavMeshAgent nm;
    public Transform target;
    public float distanceThreshold = 10f;
    public float attackThreshold = 8f;
    public float attackCooldown = 1.5f; 
    private float lastAttackTime = 0f;
    public enum AIState{idle,chasing,attack,};

    public AIState aiState = AIState.idle;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
    
        nm = GetComponent<NavMeshAgent>();
        StartCoroutine(Think());
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Think()
    {
        while(true)
        {
            if (nm == null || !nm.isOnNavMesh)
            {
                yield break; // Exit the coroutine if the NavMeshAgent is invalid
            }
            switch (aiState)
            {
                case AIState.idle:
                    float dist = Vector3.Distance(target.position,transform.position);
                    if (dist < distanceThreshold)
                    {
                        aiState = AIState.chasing;
                        animator.SetBool("Chasing", true);
                    }
                    nm.SetDestination(transform.position);
                    break;
                case AIState.chasing:
                    dist = Vector3.Distance(target.position, transform.position);
                    
                    if(dist > distanceThreshold)
                    {
                        aiState = AIState.idle;
                        animator.SetBool("Chasing",false);

                    }
                    dist = Vector3.Distance(target.position, transform.position);
                    //Debug.Log(dist);
                    if(dist < 1.5f)
                    {
                        Debug.Log("AttackThreshold met");

                        //go into attack state
                        aiState = AIState.attack;
                        animator.SetBool("Attacking",true);
                    }
                    nm.SetDestination(target.position);
                    break;
                case AIState.attack:
                    
                    nm.SetDestination(transform.position);
                    dist = Vector3.Distance(target.position, transform.position);
                    if(dist > 1.5f)
                    {
                        aiState = AIState.chasing;
                        animator.SetBool("Attacking", false);
                        
                    } else
                    {
                        if (Time.time > lastAttackTime + attackCooldown)
                        {
                            AttackPlayer();
                            lastAttackTime = Time.time;
                        }
                    }
                    break;
                    
                    //attack
                default:
                    break;
            }

            yield return new WaitForSeconds(0.2f);

        }
        
    }

    void AttackPlayer(){
        Debug.Log("Zombie attacks the player!");

        // Check if the player has a PlayerHealth component
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(34); // Damage amount
        }
    }
    
}
