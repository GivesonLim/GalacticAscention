using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;      // Player movement speed
    public float rotationSpeed = 10f; // Speed at which the player rotates to face the mouse

    public GameObject projectilePrefab;  // Reference to the projectile prefab
    public Transform firePoint;         // FirePoint where the projectile will spawn

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            Debug.Log("Spacebar pressed! Shooting projectile..."); // Add this debug log
            ShootProjectile();
        }
    }

    // Rotate the ship to face the mouse position smoothly
    void RotateToMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Get mouse position in world space
        Vector3 direction = (mousePos - transform.position).normalized; // Direction from player to mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Get angle between the two points in degrees

        // Adjust the angle so the ship is facing the mouse position correctly
        angle -= 90f; // Offset the rotation by 90 degrees to fix orientation

        // Smoothly rotate the player ship to face the mouse
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle); // Create a target rotation
        rb.rotation = Mathf.LerpAngle(rb.rotation, targetRotation.eulerAngles.z, rotationSpeed * Time.deltaTime); // Smoothly rotate towards the target angle
    }

    // WASD movement control
    void MoveWithWASD()
    {
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow keys
        float moveY = Input.GetAxis("Vertical");   // W/S or Up/Down Arrow keys

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        rb.linearVelocity = moveDirection * moveSpeed; // Fixed typo: should be velocity instead of linearVelocity
    }

    // Shoot a projectile in the direction the player is facing (towards the mouse)
    void ShootProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // Instantiate the projectile at the firePoint position and firePoint's rotation
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Debug.Log("Projectile Fired!");
        }
        else
        {
            Debug.LogError("Projectile Prefab or FirePoint not assigned!");
        }
    }
}
