using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float baseSpeed = 4f;         // Starting speed of the meteor
    public float speedPerWave = 0.5f;    // How much the speed increases per wave
    public float rotationSpeed = 180f;   // Rotation speed in degrees per second
    public float lifetime = 6f;          // Time before the meteor gets destroyed

    private Vector2 moveDirection;       // Direction the meteor moves in
    private float moveSpeed;             // Speed of the meteor

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Get the direction from the meteor to the player's current position
            Vector2 direction = (player.transform.position - transform.position).normalized;
            moveDirection = direction;
        }

        // Scale speed by current wave number (based on the WaveManager)
        WaveManager waveManager = FindObjectOfType<WaveManager>();
        int currentWave = waveManager != null ? waveManager.CurrentWaveNumber() : 1;
        moveSpeed = baseSpeed + speedPerWave * (currentWave - 1);  // Speed increases with each wave

        // Destroy the meteor after its lifetime
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move the meteor in the direction of the player
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // Rotate the meteor while it moves
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(30);  // Adjust damage as needed

                // Trigger camera shake when the meteor hits the player
                if (player.cameraShake != null)
                {
                    player.cameraShake.TriggerShake();  // Trigger the shake effect on the camera
                }
            }
        }
    }
}
