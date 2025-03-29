using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;      // Speed of the enemy movement
    public float stopDistance = 5f;   // Distance from the player at which the enemy will stop
    public Transform player;          // Reference to the player
    public GameObject enemyProjectilePrefab;  // Reference to the enemy projectile prefab
    public Transform shootPoint;              // Point where the projectile is shot from

    private bool isInRange = false;
    private ScoreManager scoreManager;

    void Start()
    {
        // Get the ScoreManager to update the score
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        // Move the enemy towards the player until within stopDistance
        if (player != null)
        {
            // Calculate the direction to the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Rotate the enemy to face the player
            RotateTowardsPlayer(direction);

            // Move the enemy towards the player if not within range
            if (Vector3.Distance(transform.position, player.position) > stopDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                // If the enemy is in range, stop and start shooting
                if (!isInRange)
                {
                    isInRange = true;
                    StartShooting(); // Start shooting projectiles at the player
                }
            }
        }
    }

    // Rotate the enemy to face the player
    void RotateTowardsPlayer(Vector3 direction)
    {
        // Calculate the angle to the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));  // Adjust by 90 degrees to face the right direction
    }

    // Call this function when the enemy enters range to start shooting
    void StartShooting()
    {
        InvokeRepeating("ShootProjectile", 0f, 1f);  // Shoot every 1 second
    }

    // Shoot the projectile at the player
    void ShootProjectile()
    {
        if (player != null && shootPoint != null)
        {
            // Instantiate the projectile at the shootPoint and aim at the player
            GameObject projectile = Instantiate(enemyProjectilePrefab, shootPoint.position, Quaternion.identity);

            // Check if the projectile was instantiated correctly
            if (projectile != null)
            {
                // Set the player's position as the target for the projectile
                EnemyProjectile projectileScript = projectile.GetComponent<EnemyProjectile>();
                if (projectileScript != null)
                {
                    projectileScript.target = player;  // Assign the target (player)
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

    // Handle collision with the player or a player projectile
    void OnTriggerEnter2D(Collider2D other)
    {
        // If the enemy collides with a player projectile
        if (other.CompareTag("PlayerProjectile"))
        {
            // Destroy the enemy and the projectile
            Destroy(gameObject);  // Destroy the enemy
            Destroy(other.gameObject);  // Destroy the player projectile

            // Update the score
            if (scoreManager != null)
            {
                scoreManager.AddScore(100);  // Add 100 points for destroying an enemy
            }

            Debug.Log("Enemy destroyed by projectile");
        }

        // If the enemy collides with the player
        if (other.CompareTag("Player"))
        {
            // Destroy the enemy
            Destroy(gameObject);  // Destroy the enemy

            // Optionally, destroy the player or handle player damage here
            Debug.Log("Enemy collided with player and was destroyed");
        }
    }
}
