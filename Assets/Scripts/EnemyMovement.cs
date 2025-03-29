using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;      // Base speed of the enemy
    public float stopDistance = 5f;   // Distance from the player at which the enemy will stop
    public Transform player;          // Reference to the player
    public GameObject enemyProjectilePrefab;  // Reference to the enemy projectile prefab
    public Transform shootPoint;              // Point where the projectile is shot from

    private bool isInRange = false;
    private ScoreManager scoreManager;

    private float speedMultiplier = 1f; // Multiplier added for scaling speed

    void Start()
    {
        // Get the ScoreManager to update the score
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            RotateTowardsPlayer(direction);

            if (Vector3.Distance(transform.position, player.position) > stopDistance)
            {
                // Apply speed multiplier here
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * speedMultiplier * Time.deltaTime);
            }
            else
            {
                if (!isInRange)
                {
                    isInRange = true;
                    StartShooting();
                }
            }
        }
    }

    void RotateTowardsPlayer(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
    }

    void StartShooting()
    {
        InvokeRepeating("ShootProjectile", 0f, 1f);
    }

    void ShootProjectile()
    {
        if (player != null && shootPoint != null)
        {
            GameObject projectile = Instantiate(enemyProjectilePrefab, shootPoint.position, Quaternion.identity);

            if (projectile != null)
            {
                EnemyProjectile projectileScript = projectile.GetComponent<EnemyProjectile>();
                if (projectileScript != null)
                {
                    projectileScript.target = player;
                }
                else
                {
                    Debug.LogError("EnemyProjectile script missing on the projectile prefab!");
                }
            }
            else
            {
                Debug.LogError("Projectile not instantiated!");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerProjectile"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);

            if (scoreManager != null)
            {
                scoreManager.AddScore(100);
            }

            Debug.Log("Enemy destroyed by projectile");
        }

        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Debug.Log("Enemy collided with player and was destroyed");
        }
    }

    // ✅ Called by spawner to scale enemy speed per wave
    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}
