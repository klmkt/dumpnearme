using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public GameObject GameOverMenu; // Assign di Inspector

    private void Start()
    {
        GameOverMenu.SetActive(false); // Sembunyikan saat game mulai
    }

    public void ShowGameOver()
    {
        GameOverMenu.SetActive(true); // Tampilkan UI Game Over
        Time.timeScale = 0f; // Pause game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Kembalikan waktu normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Kembalikan waktu normal
        SceneManager.LoadScene(0); // Load scene utama (scene index 0)
    }
}
