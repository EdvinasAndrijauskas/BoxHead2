using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject scoreText;

    void Start()
    {
        TextMeshProUGUI textMeshProLable = scoreText.GetComponent<TextMeshProUGUI>();
        textMeshProLable.text = "Score: " + Score.points;
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        SceneChanger.toTriggerFadeOut = false;
        PlayerHealth.isDead = false;
        Score.points = 0;
        Debug.Log("RESTART");
    }

    public static IEnumerator GameOverScene()
    {
        if (!PlayerHealth.isDead) yield break;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
        Score.points = 0;
    }
}