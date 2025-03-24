using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton pattern for easy access

    public int currentScore = 0;
    public int highScore = 0;
    private int wrongHitsInARow = 0;
    private int lostPointsInARow = 0;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }

        // Load high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void AddScore(int points)
    {
        currentScore += points;
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        Debug.Log($"Added {points} points. Current Score: {currentScore}, High Score: {highScore}");
    }

    public void SubtractScore(int points)
    {
        currentScore -= points;
        if (currentScore < 0) currentScore = 0;
        Debug.Log($"Subtracted {points} points. Current Score: {currentScore}, High Score: {highScore}");
    }

    public void ResetScore()
    {
        currentScore = 0;
        wrongHitsInARow = 0;
        lostPointsInARow = 0;
    }

    public void HandleWrongHit()
    {
        wrongHitsInARow++;
        if (wrongHitsInARow >= 3)
        {
            SubtractScore(1); // Subtract 1 point for every wrong hit after 3 in a row
        }
    }

    public void HandleLostTrash()
    {
        SubtractScore(1);
        lostPointsInARow++;
        if (lostPointsInARow >= 5)
        {
            GameOver();
        }
    }

    public void ResetWrongHits()
    {
        wrongHitsInARow = 0;
    }

    public void ResetLostPoints()
    {
        lostPointsInARow = 0;
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        // Add game over logic here (e.g., show game over screen, restart game, etc.)
    }
}