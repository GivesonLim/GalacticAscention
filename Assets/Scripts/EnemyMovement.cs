using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;      // Base speed of the enemy
    public float stopDistance = 5f;   // Distance from the player at which the enemy will stop
    public Transform player;          // Reference to the player
    public GameObject enemyProjectilePrefab;  // Reference to the enemy projectile prefab
    public Transform shootPoint;              // Point where the projectile is shot from

    public GameObject explosionEffect;    // Reference to the static explosion effect prefab
    public AudioClip explosionSFX;    // Explosion sound effect
    private bool isInRange = false;
    private ScoreManager scoreManager;

    private float speedMultiplier = 1f;
    private bool isDestroyed = false;  // Flag to track if the enemy is destroyed

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>(); // Get the score manager
    }

    void Update()
    {
        if (isDestroyed) return;  // Stop movement if the enemy is destroyed

        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            RotateTowardsPlayer(direction);

            if (Vector3.Distance(transform.position, player.position) > stopDistance)
            {
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
            EnemyProjectile projectileScript = projectile.GetComponent<EnemyProjectile>();
            if (projectileScript != null)
            {
                projectileScript.target = player;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerProjectile"))
        {
            PlayExplosionEffect();   // Play the explosion effect
            DestroyEnemy();  // Call method to destroy the enemy
            Destroy(other.gameObject);  // Destroy the player projectile

            if (scoreManager != null)
            {
                scoreManager.AddScore(100);  // Add score
            }
        }

        if (other.CompareTag("Player"))
        {
            PlayExplosionEffect();   // Play the explosion effect
            DestroyEnemy();  // Call method to destroy the enemy if it collides with the player
        }
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }

    void PlayExplosionEffect()
    {
        if (explosionEffect != null && explosionSFX != null)
        {
            // Instantiate the explosion effect at the enemy's position
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);

            // Play the explosion sound immediately
            AudioSource.PlayClipAtPoint(explosionSFX, transform.position);

            // Optionally set the parent to the enemy so it follows the enemy's destruction
            explosion.transform.parent = transform;

            // Destroy the explosion effect after 1 second
            Destroy(explosion, 1f);  // Adjust the duration as needed
        }
    }

    void DestroyEnemy()
    {
        isDestroyed = true;  // Set the flag to stop movement
        Destroy(gameObject, 0.1f);  // Short delay before destruction to allow sound and explosion to finish
    }
}
