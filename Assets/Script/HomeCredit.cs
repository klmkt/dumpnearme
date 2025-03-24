using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditMenu : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
