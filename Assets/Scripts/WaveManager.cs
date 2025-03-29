using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    public int totalWaves = 5;
    public float waveDuration = 120f; // 2 minutes per wave

    [Header("UI")]
    public Text timerText;
    public Text waveText;

    [Header("Difficulty Scaling")]
    public EnemySpawner enemySpawner; // Assign in Inspector
    public float enemySpawnRateMultiplier = 0.9f; // Spawn faster each wave

    private float currentWaveTime;
    private int currentWave = 1;
    private bool waveActive = true;

    void Start()
    {
        StartWave();
    }

    void Update()
    {
        if (!waveActive) return;

        currentWaveTime -= Time.deltaTime;
        UpdateTimerUI();

        if (currentWaveTime <= 0f)
        {
            NextWave();
        }
    }

    void StartWave()
    {
        Debug.Log("Wave " + currentWave + " started.");

        currentWaveTime = waveDuration;
        waveActive = true;
        UpdateWaveUI();

        // Increase difficulty
        if (enemySpawner != null)
        {
            enemySpawner.spawnRate *= enemySpawnRateMultiplier;
        }
    }

    void NextWave()
    {
        currentWave++;

        if (currentWave > totalWaves)
        {
            waveActive = false;
            Debug.Log("All waves complete!");
            GameOverWin();
            return;
        }

        StartWave();
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentWaveTime / 60);
            int seconds = Mathf.FloorToInt(currentWaveTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void UpdateWaveUI()
    {
        if (waveText != null)
        {
            waveText.text = "Wave: " + currentWave;
        }
    }

    void GameOverWin()
    {
        Debug.Log("YOU WIN!");
        if (timerText != null)
            timerText.text = "00:00";

        if (waveText != null)
            waveText.text = "All Waves Complete";

        Time.timeScale = 0f;
    }
}
