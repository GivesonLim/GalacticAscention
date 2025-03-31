using UnityEngine;
using UnityEngine.SceneManagement;  // Needed for scene management
using UnityEngine.UI; // Needed for working with Text UI

public class WinningSceneManager : MonoBehaviour
{
    public Text finalScoreText; // Reference to the UI Text that displays the final score
    public ScoreManager scoreManager; // Reference to ScoreManager to get the final score

    public void RestartGame()
    {
        scoreManager.ResetScore();  // Reset the score before restarting
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Scene"); // Restart the current scene
    }

    public void ReturnToMainMenu()
    {
        scoreManager.ResetScore();  // Reset the score before going to the main menu
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu"); // Go to the main menu (make sure to use the correct scene name)
    }

    // Method to display final score after player wins
    public void ShowFinalScore()
    {
        if (finalScoreText != null && scoreManager != null)
        {
            // Retrieve the final score from the ScoreManager or PlayerPrefs
            int finalScore = PlayerPrefs.GetInt("FinalScore", 0); // Default to 0 if not found
            finalScoreText.text = "Final Score: " + finalScore.ToString(); // Display final score
        }
    }
}
