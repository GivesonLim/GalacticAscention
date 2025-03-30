using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Needed for working with Text UI

public class GameOverManager : MonoBehaviour
{
    public Text FinalScoreText; // UI text element to show the final score
    public ScoreManager scoreManager; // Reference to ScoreManager to get the final score

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart the current scene
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu"); // Go to the main menu (make sure to use the correct scene name)
    }

    // Method to display final score after player dies
    public void ShowFinalScore()
    {
        if (FinalScoreText != null && scoreManager != null)
        {
            FinalScoreText.text = "Final Score: " + scoreManager.GetScore().ToString(); // Display final score
        }
    }
}
