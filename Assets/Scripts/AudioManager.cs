using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip gameSceneMusic;  // Reference to the background music for the game scene
    private AudioSource audioSource;

    void Start()
    {
        // Check if the AudioSource component is already on the object
        audioSource = GetComponent<AudioSource>();
        
        // If no AudioSource, we add one dynamically
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set the clip to the background music for the game scene
        audioSource.clip = gameSceneMusic;

        // Play the music and set it to loop
        audioSource.loop = true;
        audioSource.Play();
    }
}
