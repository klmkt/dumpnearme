using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerScore : MonoBehaviour
{
    private Text scoreText;

    private int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreText.text = "0";
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Trash")
        {
            target.gameObject.SetActive(false);
            score++;
            scoreText.text = score.ToString();
        }
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
