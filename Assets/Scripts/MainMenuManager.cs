using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public AudioSource audioSource;  // Reference to the AudioSource component
    public AudioClip buttonClickSound; // Reference to the button click sound effect
    public AudioClip backgroundMusic;  // Reference to the background music

    void Start()
    {
        // Ensure the background music starts when the menu is displayed
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayGame()
    {
        audioSource.Stop();  // Stop background music when transitioning
        audioSource.PlayOneShot(buttonClickSound); // Play the button click sound
        SceneManager.LoadScene("Main_Scene"); // Use the exact name from Build Settings
    }

    public void QuitGame()
    {
        audioSource.Stop();  // Stop background music when quitting the game
        audioSource.PlayOneShot(buttonClickSound); // Play the button click sound
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
