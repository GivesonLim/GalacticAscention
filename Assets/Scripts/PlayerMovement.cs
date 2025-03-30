using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;      // Player movement speed
    public float rotationSpeed = 10f; // Speed at which the player rotates to face the mouse

    public GameObject projectilePrefab;  // Reference to the projectile prefab
    public Transform firePoint;         // FirePoint where the projectile will spawn

    public AudioClip playerDestroyedSound;  // Sound effect for player destruction
    public AudioClip playerDamagedSound;    // Sound effect for player damage
    private AudioSource audioSource;        // AudioSource component to play the sound

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>(); // Get AudioSource component
    }

    void Update()
    {
        // Rotate the ship to follow the mouse position
        RotateToMouse();

        // Move the player using WASD
        MoveWithWASD();

        // Shoot the projectile when spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootProjectile();
        }
    }

    void RotateToMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Get mouse position in world space
        Vector3 direction = (mousePos - transform.position).normalized; // Direction from player to mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Get angle between the two points in degrees

        // Adjust the angle so the ship is facing the mouse position correctly
        angle -= 90f;

        // Smoothly rotate the player ship to face the mouse
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        rb.rotation = Mathf.LerpAngle(rb.rotation, targetRotation.eulerAngles.z, rotationSpeed * Time.deltaTime);
    }

    void MoveWithWASD()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        rb.linearVelocity = moveDirection * moveSpeed; // Move the player
    }

    void ShootProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation); // Shoot the projectile
        }
    }

    // Call this method when the player collides with something that destroys it
    public void DestroyPlayer()
    {
        // Play the player destruction sound
        if (audioSource != null && playerDestroyedSound != null)
        {
            audioSource.PlayOneShot(playerDestroyedSound);
        }

        // Destroy the player (you can add any other logic here, like playing a death animation)
        Destroy(gameObject);
    }

    // Method for taking damage
    public void TakeDamage(int damage)
    {
        // Play the player damage sound
        if (audioSource != null && playerDamagedSound != null)
        {
            audioSource.PlayOneShot(playerDamagedSound);
        }

        // You can subtract health here (if you have a health system)
        // For now, we will just print a message
        Debug.Log("Player took damage!");
    }
}
