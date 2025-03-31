using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;  // The text for displaying the score during gameplay
    public Text finalScoreText;  // The text for displaying the final score in the end scenes
    private int score;

    void Start()
    {
        // Load the score from PlayerPrefs when the game starts, default to 0 if no score is found
        score = PlayerPrefs.GetInt("FinalScore", 0);  
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        PlayerPrefs.SetInt("FinalScore", score);  // Save the score to PlayerPrefs
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;  // Update the score on the gameplay UI
        }

        if (finalScoreText != null)
        {
            finalScoreText.text = "Final Score: " + score;  // Update the score on the GameOver/Victory UI
        }
    }

    // Method to reset the score at the beginning of a new game
    public void ResetScore()
    {
        score = 0;
        PlayerPrefs.DeleteKey("FinalScore");  // Delete the stored score in PlayerPrefs to reset it
        UpdateScoreUI();
    }

    // Optionally, when the game ends
    public void GameOver()
    {
        PlayerPrefs.SetInt("FinalScore", score);  // Save the score when the game ends
    }

    public int GetScore()
    {
        return score;
    }
}
