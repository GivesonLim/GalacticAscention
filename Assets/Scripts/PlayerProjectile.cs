using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float speed = 10f;  // Speed of the projectile
    public float lifeTime = 5f;  // Time before the projectile is destroyed if not hitting anything

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component for movement

        // Set the velocity for the projectile
        rb.linearVelocity = transform.up * speed;  // Assuming the projectile moves in the direction the ship is facing

        // Destroy the projectile after the lifetime
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);  // Destroy the enemy when hit by the projectile
            Destroy(gameObject);  // Destroy the projectile
        }
    }
}
