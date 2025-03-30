using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;      // Base speed of the enemy
    public float stopDistance = 5f;   // Distance from the player at which the enemy will stop
    public Transform player;          // Reference to the player
    public GameObject enemyProjectilePrefab;  // Reference to the enemy projectile prefab
    public Transform shootPoint;              // Point where the projectile is shot from

    public AudioClip enemyDestroyedSound;  // Sound for when the enemy is destroyed
    private AudioSource audioSource;        // AudioSource component to play the sound

    private bool isInRange = false;
    private ScoreManager scoreManager;

    private float speedMultiplier = 1f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get AudioSource component
        scoreManager = FindObjectOfType<ScoreManager>(); // Get the score manager
    }

    void Update()
    {
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
            // Play enemy destruction sound when destroyed by projectile
            if (audioSource != null && enemyDestroyedSound != null)
            {
                audioSource.PlayOneShot(enemyDestroyedSound);
            }

            Destroy(gameObject);  // Destroy the enemy
            Destroy(other.gameObject);  // Destroy the player projectile

            if (scoreManager != null)
            {
                scoreManager.AddScore(100);
            }
        }

        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);  // Destroy the enemy if it collides with the player
        }
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}
