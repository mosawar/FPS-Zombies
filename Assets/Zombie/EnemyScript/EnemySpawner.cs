using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // The zombie prefab to spawn
    public Transform[] spawnPoints; // Array of spawn points
    public float spawnInterval = 5f; // Time between spawns
    public int maxZombies = 10; // Maximum number of zombies in the scene
    public TextMeshProUGUI killCount;
    public  TextMeshProUGUI time;
    public int kills;
    public float elapsedTime;

    private int currentZombieCount = 0;

    void Start()
    {
        StartCoroutine(SpawnZombies());
        killCount.text = "Zombies Killed: " + kills;
        time.text = "Time Survived: 0";

    }
    
    void Update(){
        elapsedTime += Time.deltaTime;
       

        int seconds = Mathf.FloorToInt(elapsedTime);

        
        time.text = "Time Survived: " + seconds;

    }
    IEnumerator SpawnZombies()
    {
        while (true)
        {
            // Wait for the specified interval
            yield return new WaitForSeconds(spawnInterval);

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

        // Subscribe to the zombie's death event to reduce the count when it's destroyed
        zombie.GetComponent<EnemyHealth>().onDeath += OnZombieDeath;
    }

    void OnZombieDeath()
    {
        // Decrement the zombie count when a zombie dies
        kills++;

        killCount.text = "Zombies Killed: " + kills;
        currentZombieCount--;

}}
