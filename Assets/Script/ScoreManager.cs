using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int currentScore = 0;
    public int highScore = 0;
    private int wrongHitsInARow = 0;
    private int lostTrashesInARow = 0; // Track consecutive lost trashes
    public bool isGameOver = false;

    [Header("UI References")]
    public GameObject gameOverPanel;
    public TMP_Text finalScoreText;
    public TMP_Text finalHighScoreText;
    public ScoreDisplay scoreDisplay;

    [Header("Game Settings")]
    public int maxWrongHits = 5; // Game over after 5 wrong bin hits
    public int maxLostTrashes = 5; // Game over after 5 lost trashes

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (scoreDisplay == null)
        {
            scoreDisplay = FindObjectOfType<ScoreDisplay>();
        }
    }

    public void AddScore(int points)
    {
        if (isGameOver) return;

        currentScore += points;
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        UpdateScoreUI();
        ResetWrongHits();
        ResetLostTrashes(); // Reset both counters on correct hit
    }

    public void SubtractScore(int points)
    {
        if (isGameOver) return;

        currentScore -= points;
        if (currentScore < 0) currentScore = 0;
        UpdateScoreUI();
    }

    public void HandleWrongHit()
    {
        if (isGameOver) return;

        wrongHitsInARow++;
        lostTrashesInARow = 0; // Reset lost trash counter

        Debug.Log($"Wrong bin hit! Consecutive: {wrongHitsInARow}/{maxWrongHits}");

        SubtractScore(1); // Deduct point for wrong hit

        // if (wrongHitsInARow >= maxWrongHits)
        // {
        //     GameOver("Too many wrong bin hits!");
        // }
    }

    public void HandleLostTrash()
    {
        if (isGameOver) return;

        lostTrashesInARow++;
        wrongHitsInARow = 0; // Reset wrong hit counter

        Debug.Log($"Trash lost! Consecutive: {lostTrashesInARow}/{maxLostTrashes}");

        SubtractScore(1); // Deduct point for lost trash

        // if (lostTrashesInARow >= maxLostTrashes)
        // {
        //     GameOver("Too many trashes missed!");
        // }
    }

    private void UpdateScoreUI()
    {
        if (scoreDisplay != null)
        {
            scoreDisplay.UpdateScoreDisplay(currentScore, highScore);
        }
    }

    private void GameOver(string reason)
    {
        isGameOver = true;

        Debug.Log($"Game Over: {reason}");

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            if (finalScoreText != null)
                finalScoreText.text = $"Final Score: {currentScore}";
            if (finalHighScoreText != null)
                finalHighScoreText.text = $"High Score: {highScore}";
        }

        Time.timeScale = 0f; // Pause game AFTER showing UI

        Debug.Log($"Game Over triggered because: {reason}");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetScore()
    {
        currentScore = 0;
        ResetWrongHits();
        ResetLostTrashes();
        UpdateScoreUI();
    }

    public void ResetWrongHits()
    {
        wrongHitsInARow = 0;
        Debug.Log("Reset wrong hits counter");
    }

    public void ResetLostTrashes()
    {
        lostTrashesInARow = 0;
        Debug.Log("Reset lost trashes counter");
    }
}