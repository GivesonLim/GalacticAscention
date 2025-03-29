using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 2f;
    public Transform player;

    [HideInInspector]
    public float enemySpeedMultiplier = 1f;

    private Camera mainCamera;
    private float timer;

    void Start()
    {
        mainCamera = Camera.main;
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
        float screenWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float screenHeight = mainCamera.orthographicSize;

        Vector3 spawnPosition = new Vector3();

        if (Random.value > 0.5f)
        {
            spawnPosition.x = Random.Range(-screenWidth * 5f, screenWidth * 5f);
            spawnPosition.y = Random.Range(-screenHeight * 2f, screenHeight * 2f);
        }
        else
        {
            spawnPosition.x = Random.Range(-screenWidth * 2f, screenWidth * 2f);
            spawnPosition.y = Random.Range(-screenHeight * 5f, screenHeight * 5f);
        }

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        enemyMovement.player = player;
        enemyMovement.SetSpeedMultiplier(enemySpeedMultiplier); // 👈 Apply speed multiplier
    }
}
