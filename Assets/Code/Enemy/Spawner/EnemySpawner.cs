using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;        // Enemy prefab to spawn
    public Transform[] spawnPoints;       // Array of spawn points
    public float spawnInterval = 5f;      // Time interval between spawns
    public int maxEnemies = 10;           // Maximum number of enemies allowed in the scene

    private int currentEnemyCount = 0;

    void Start()
    {
        // Start spawning enemies
        InvokeRepeating("SpawnEnemy", 2f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Only spawn if current number of enemies is below the limit
        if (currentEnemyCount < maxEnemies)
        {
            // Pick a random spawn point
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            // Instantiate the enemy at the spawn point
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            // Increase enemy count
            currentEnemyCount++;
        }
    }

    // Decrease the enemy count when an enemy is destroyed
    public void DecreaseEnemyCount()
    {
        currentEnemyCount--;
    }
}
