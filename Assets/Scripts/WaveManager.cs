using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    public int totalWaves = 5;
    public float waveDuration = 120f;

    [Header("UI")]
    public Text timerText;
    public Text waveText;
    public Text waveBannerText;

    [Header("Difficulty Scaling")]
    public EnemySpawner enemySpawner;
    public float enemySpawnRateMultiplier = 0.9f;
    public float enemySpeedMultiplierPerWave = 1.05f; // +5% per wave

    private float currentWaveTime;
    private int currentWave = 1;
    private bool waveActive = true;
    private float currentSpeedMultiplier = 1f;

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

        if (enemySpawner != null)
        {
            enemySpawner.spawnRate *= enemySpawnRateMultiplier;
            currentSpeedMultiplier *= enemySpeedMultiplierPerWave;
            enemySpawner.enemySpeedMultiplier = currentSpeedMultiplier;
        }

        if (waveBannerText != null)
        {
            StartCoroutine(AnimateWaveBanner("WAVE " + currentWave));
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

        if (waveBannerText != null)
            waveBannerText.text = "All Waves Complete";

        Time.timeScale = 0f;
    }

    IEnumerator AnimateWaveBanner(string message)
    {
        waveBannerText.text = message;

        Color color = waveBannerText.color;
        color.a = 0;
        waveBannerText.color = color;

        while (waveBannerText.color.a < 1f)
        {
            color.a += Time.deltaTime * 2f;
            waveBannerText.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);

        while (waveBannerText.color.a > 0f)
        {
            color.a -= Time.deltaTime * 2f;
            waveBannerText.color = color;
            yield return null;
        }
    }
}
