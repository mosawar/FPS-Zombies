using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // The zombie prefab to spawn
    public Transform[] spawnPoints; // Array of spawn points
    public float spawnInterval = 10f; // Time between spawns
    public int maxZombies = 10; // Maximum number of zombies in the scene
    private int currentZombieCount = 0;
    private PlayerScore playerScore; // Cached reference to the PlayerScore script

    void Start()
    {
        // Cache the PlayerScore reference at the start
        playerScore = FindObjectOfType<PlayerScore>();
        if (playerScore == null)
        {
            Debug.LogError("PlayerScore script not found in the scene.");
        }
        StartCoroutine(SpawnZombies());
    }

    IEnumerator SpawnZombies()
    {
        while (true)
        {
            // Wait for the specified interval
            yield return new WaitForSeconds(6f);

            // Check if we can spawn more zombies
            if (currentZombieCount < maxZombies)
            {
                SpawnZombie();

            }
        }
    }

    void SpawnZombie()
    {
        // Choose a random spawn point
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Instantiate the zombie at the chosen spawn point
        GameObject zombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);

        

        // Increment the zombie count
        currentZombieCount++;
        // Subscribe to the zombie's death event
        EnemyHealth enemyHealth = zombie.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.onDeath += OnZombieDeath;
            if (playerScore != null)
            {
                enemyHealth.onDeath += playerScore.IncrementZombiesKilled;
            }
        }
    }

    void OnZombieDeath()
    {
        // Decrement the zombie count when a zombie dies
        currentZombieCount--;
    }
}
