using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTImer : MonoBehaviour
{
    public float timeRemaining = 90f;
    public bool timerIsRunning = false;
    public TMP_Text timerText;

    void Start()
    {
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                Debug.Log("Time is up!");
                timeRemaining = 0;
                timerIsRunning = false;
                UpdateTimerDisplay(timeRemaining);
                TriggerGameOver();
            }
        }
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        if (timerText != null)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void TriggerGameOver()
    {
        // Beri tahu ScoreManager bahwa game sudah berakhir
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.isGameOver = true;
            
            // Tampilkan panel game over dari ScoreManager jika ada
            if (ScoreManager.Instance.gameOverPanel != null)
            {
                ScoreManager.Instance.gameOverPanel.SetActive(true);
            }
            if (ScoreManager.Instance.finalScoreText != null)
                ScoreManager.Instance.finalScoreText.text = $"Final Score: {ScoreManager.Instance.currentScore}";
            if (ScoreManager.Instance.finalHighScoreText != null)
                ScoreManager.Instance.finalHighScoreText.text = $"High Score: {ScoreManager.Instance.highScore}";
        }

        // Coba tampilkan GameOverUI jika ada di scene
        GameOverUI gameOverUI = FindObjectOfType<GameOverUI>();
        if (gameOverUI != null)
        {
            gameOverUI.ShowGameOver();
        }
        else
        {
            // Jika tidak ada GameOverUI, hentikan waktu secara manual
            Time.timeScale = 0f;
        }
    }
}
