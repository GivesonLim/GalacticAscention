using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;  // UI Text element to display the score
    private int score = 0;

    // Method to add points to the score
    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score.ToString();  // Update the score UI text
    }
}
