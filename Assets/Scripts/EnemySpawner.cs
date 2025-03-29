using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // Reference to the enemy prefab
    public float spawnRate = 2f;    // Time between spawns (now controllable by WaveManager)
    public Transform player;        // Reference to the player

    private Camera mainCamera;
    private float timer;

    void Start()
    {
        mainCamera = Camera.main;  // Get the main camera reference
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        // Get the screen bounds in world space
        float screenWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float screenHeight = mainCamera.orthographicSize;

        // Randomly choose where to spawn the enemy (way outside of the visible screen)
        Vector3 spawnPosition = new Vector3();

        if (Random.value > 0.5f) // Left or right
        {
            spawnPosition.x = Random.Range(-screenWidth * 5f, screenWidth * 5f);
            spawnPosition.y = Random.Range(-screenHeight * 2f, screenHeight * 2f);
        }
        else // Top or bottom
        {
            spawnPosition.x = Random.Range(-screenWidth * 2f, screenWidth * 2f);
            spawnPosition.y = Random.Range(-screenHeight * 5f, screenHeight * 5f);
        }

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        enemyMovement.player = player;
    }
}
