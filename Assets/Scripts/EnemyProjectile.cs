using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 5f;      // Speed of the projectile
    public Transform target;      // The player, target of the projectile
    public float lifeTime = 5f;   // Time before the projectile is destroyed if it doesn't hit anything
    public AudioClip shootSound;  // Reference to the shooting sound effect
    private AudioSource audioSource;  // AudioSource to play the sound

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component for movement
        audioSource = GetComponent<AudioSource>();  // Get the AudioSource component

        // Play the shooting sound when the enemy fires a projectile
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);  // Play the shoot sound
        }

        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;  // Move towards the player

            // Rotate the projectile to face the direction it's traveling
            RotateProjectile(direction);
        }

        // Destroy the projectile after a certain amount of time if it doesn't hit anything
        Destroy(gameObject, lifeTime);
    }

    void RotateProjectile(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10); // Deal damage to player
            }

            Destroy(gameObject); // Destroy projectile only
        }
        else if (!collision.CompareTag("Enemy"))
        {
            Destroy(gameObject); // Clean up on any non-enemy collision
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); // Cleanup off-screen
    }
}
