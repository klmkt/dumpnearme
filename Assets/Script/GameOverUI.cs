using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject GameOverMenu; // Parent object containing all game over UI
    public Button homeButton;
    public Button restartButton;

    [Header("Debug Settings")]
    [Tooltip("Enable to check for visibility issues")]
    public bool debugVisibility = true;

    private void Start()
    {
        // Initialize UI state
        if (GameOverMenu != null)
        {
            GameOverMenu.SetActive(false);
        }

        // Set up button listeners
        if (homeButton != null)
        {
            homeButton.onClick.AddListener(GoToMainMenu);
        }

        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }
    }

    public void ShowGameOver()
    {
        if (GameOverMenu == null)
        {
            Debug.LogError("GameOverMenu reference is not set!");
            return;
        }

        GameOverMenu.SetActive(true);
        Time.timeScale = 0f;

#if UNITY_EDITOR
        // Force visibility check when showing
        if (debugVisibility)
        {
            DebugCheckPanelVisibility();
        }
#endif
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // Load main menu (scene 0)
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (debugVisibility)
        {
            DebugCheckPanelVisibility();
        }
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    private void DebugCheckPanelVisibility()
    {
        if (GameOverMenu == null || !GameOverMenu.activeSelf) return;

        // Check all images in the hierarchy
        Image[] images = GameOverMenu.GetComponentsInChildren<Image>(true);
        foreach (Image img in images)
        {
            if (img.color.a < 0.1f)
            {
                Debug.LogWarning($"UI element {img.name} has low alpha ({img.color.a})!", img.gameObject);
            }
        }

        // Additional check for Canvas Group
        CanvasGroup canvasGroup = GameOverMenu.GetComponent<CanvasGroup>();
        if (canvasGroup != null && canvasGroup.alpha < 0.1f)
        {
            Debug.LogWarning($"CanvasGroup on {GameOverMenu.name} has low alpha ({canvasGroup.alpha})!", GameOverMenu);
        }
    }
#endif
}