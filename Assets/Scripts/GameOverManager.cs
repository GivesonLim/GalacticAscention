using UnityEngine;
using UnityEngine.SceneManagement;  // Needed for scene management
using UnityEngine.UI; // Needed for working with Text UI

public class GameOverManager : MonoBehaviour
{
    public Text FinalScoreText;  // UI Text element to show the final score
    public ScoreManager scoreManager; // Reference to ScoreManager to get the final score

    public void RestartGame()
    {
        scoreManager.ResetScore();  // Reset the score before restarting
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart the current scene
    }

    public void ReturnToMainMenu()
    {
        scoreManager.ResetScore();  // Reset the score before going to the main menu
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu"); // Go to the main menu (make sure to use the correct scene name)
    }

    // Method to display final score after player dies
    public void ShowFinalScore()
    {
        if (FinalScoreText != null)
        {
            // Retrieve final score from PlayerPrefs
            int finalScore = PlayerPrefs.GetInt("FinalScore", 0); // Default to 0 if not found
            FinalScoreText.text = "Final Score: " + finalScore.ToString(); // Display final score
        }
    }
}
