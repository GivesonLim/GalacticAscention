using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void RestartGame()
    {
        // Reloads the current game scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f; // Resume time in case it was paused
    }

    public void ReturnToMainMenu()
    {
        // Load main menu scene
        SceneManager.LoadScene("Main_Menu");
        Time.timeScale = 1f; // Resume time
    }
}
