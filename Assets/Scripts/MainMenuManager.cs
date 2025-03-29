using UnityEngine;
using UnityEngine.SceneManagement;  // For loading scenes
using UnityEngine.UI;              // For working with UI buttons

public class MainMenuManager : MonoBehaviour
{
    public Button playButton;    // Play Game Button
    public Button quitButton;    // Quit Button
    public GameObject mainMenuPanel; // Main Menu Panel UI element

    void Start()
    {
        // Add listeners to buttons
        playButton.onClick.AddListener(OnPlayButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);

        // Pause the game when the main menu is active
        Time.timeScale = 0f;  // Stops the game
    }

    // When the "Play Game" button is clicked
    void OnPlayButtonClicked()
    {
        Time.timeScale = 1f;  // Resumes the game when Play is clicked
        SceneManager.LoadScene("GameScene");  // Replace with your actual game scene name
    }

    // When the "Quit" button is clicked
    void OnQuitButtonClicked()
    {
        Debug.Log("Exiting game...");
        Application.Quit();  // Quit the game
    }
}
