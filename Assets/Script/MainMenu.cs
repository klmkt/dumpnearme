using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Reset skor sebelum memulai game (jika ScoreManager digunakan)
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }

        // Load scene gameplay (urutan 1)
        SceneManager.LoadScene(1);
    }

    public void OpenCredits()
    {
        // Load scene Credit (urutan 2)
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        // Keluar dari game (tidak berfungsi di editor)
        Application.Quit();

        // Jika dalam mode Editor, gunakan Debug.Log sebagai indikasi
#if UNITY_EDITOR
        Debug.Log("Game Keluar!");
#endif
    }
}
