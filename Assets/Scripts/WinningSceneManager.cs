using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinningSceneManager : MonoBehaviour
{
    public Text finalScoreText;  // UI Text to display the final score
    private ScoreManager scoreManager;  // Reference to the ScoreManager
    public AudioClip victoryMusic;  // Public reference to the victory music clip
    private AudioSource audioSource;  // AudioSource to play the music

    void Start()
    {
        // Try to find the ScoreManager in the scene (it will persist across scenes)
        scoreManager = FindObjectOfType<ScoreManager>();

        if (scoreManager != null && finalScoreText != null)
        {
            finalScoreText.text = "Final Score: " + scoreManager.GetScore().ToString();  // Set the final score
        }
        else
        {
            Debug.LogError("ScoreManager or FinalScoreText not assigned!");
        }

        // Set up the AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null && victoryMusic != null)
        {
            audioSource.clip = victoryMusic;  // Assign the music clip
            audioSource.loop = true;  // Set it to loop the music
            audioSource.Play();  // Play the background music
        }
        else
        {
            Debug.LogError("AudioSource or VictoryMusic not assigned!");
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;  // Resume time
        SceneManager.LoadScene("Main_Scene");  // Restart the game
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;  // Resume time
        SceneManager.LoadScene("Main_Menu");  // Go to main menu
    }
}
