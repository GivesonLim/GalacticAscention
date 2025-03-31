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
    public float enemySpeedMultiplierPerWave = 1.05f;

    [Header("Player Health")]
    public PlayerHealth playerHealth;

    [Header("Meteor Settings")]
    public GameObject meteorPrefab;
    public float baseMeteorInterval = 10f;
    private float meteorTimer = 0f;

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

        meteorTimer += Time.deltaTime;
        if (meteorTimer >= baseMeteorInterval)
        {
            SpawnMeteor();
            meteorTimer = 0f;
        }

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

        if (playerHealth != null)
        {
            playerHealth.RestoreFullHealth();
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

        baseMeteorInterval = Mathf.Max(3f, baseMeteorInterval * 0.95f);

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
            waveBannerText.text = "Victory!";

        Time.timeScale = 0f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Winning_Scene");
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

    void SpawnMeteor()
    {
        if (meteorPrefab == null || playerHealth == null)
            return;

        Vector2 spawnPos = GetRandomEdgePosition();
        Instantiate(meteorPrefab, spawnPos, Quaternion.identity);
    }

    Vector2 GetRandomEdgePosition()
    {
        Camera cam = Camera.main;
        float buffer = 1f;

        float screenLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x - buffer;
        float screenRight = cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x + buffer;
        float screenTop = cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + buffer;
        float screenBottom = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y - buffer;

        int edge = Random.Range(0, 4);
        Vector2 pos = Vector2.zero;

        switch (edge)
        {
            case 0: pos = new Vector2(Random.Range(screenLeft, screenRight), screenTop); break;
            case 1: pos = new Vector2(Random.Range(screenLeft, screenRight), screenBottom); break;
            case 2: pos = new Vector2(screenLeft, Random.Range(screenBottom, screenTop)); break;
            case 3: pos = new Vector2(screenRight, Random.Range(screenBottom, screenTop)); break;
        }

        return pos;
    }

    public int CurrentWaveNumber()
    {
        return currentWave;
    }
}
