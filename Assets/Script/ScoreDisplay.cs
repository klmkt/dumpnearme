using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text currentScoreText; // Reference to the TextMeshPro component for current score
    public TMP_Text highScoreText;   // Reference to the TextMeshPro component for high score

    void Update()
    {
        // Update the text fields with the current score and high score
        if (currentScoreText != null)
        {
            currentScoreText.text = $"Score: {ScoreManager.Instance.currentScore}";
        }
        if (highScoreText != null)
        {
            highScoreText.text = $"High Score: {ScoreManager.Instance.highScore}";
        }
    }
}