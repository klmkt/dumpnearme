using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text currentScoreText;
    public TMP_Text highScoreText;

    public void UpdateScoreDisplay(int currentScore, int highScore)
    {
        if (currentScoreText != null)
        {
            currentScoreText.text = $"Score: {currentScore}";
        }
        if (highScoreText != null)
        {
            highScoreText.text = $"High Score: {highScore}";
        }
    }
}