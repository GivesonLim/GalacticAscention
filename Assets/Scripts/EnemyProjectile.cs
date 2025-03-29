using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 5f;      // Speed of the projectile
    public Transform target;      // The player, target of the projectile
    public float lifeTime = 5f;   // Time before the projectile is destroyed if it doesn't hit anything

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component for movement

        if (target != null)
        {
            // Calculate the direction from the projectile to the player
            Vector2 direction = (target.position - transform.position).normalized;

            // Apply velocity to the projectile to move it towards the player
            rb.linearVelocity = direction * speed;

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

    // ✅ Use trigger instead of collision since we're using Kinematic rigidbody
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10); // Deal damage
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
