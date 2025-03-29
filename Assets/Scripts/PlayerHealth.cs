using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    [Header("UI")]
    public Slider healthSlider;
    public GameObject gameOverPanel;

    [Header("Hit Flash")]
    public SpriteRenderer spriteRenderer;
    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;
    public int flashCount = 3;

    [Header("Camera Shake")]
    public CameraShake cameraShake; // Assign your main camera here

    private Color originalColor;
    private bool isFlashing = false;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        if (spriteRenderer != null)
            StartCoroutine(FlashMultipleTimes());

        if (cameraShake != null)
            cameraShake.TriggerShake();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    void Die()
    {
        Debug.Log("Player Died");
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    System.Collections.IEnumerator FlashMultipleTimes()
    {
        if (isFlashing) yield break;

        isFlashing = true;

        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }

        isFlashing = false;
    }
}
