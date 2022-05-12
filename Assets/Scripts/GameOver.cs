using System.Collections;
using data;
using SFX;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        // SceneChanger.toTriggerFadeOut = false;
        PlayerHealth.isDead = false;
        Score.points = 0;
        Debug.Log("RESTART");
    }

    public static IEnumerator GameOverScene()
    {
        if (!PlayerHealth.isDead) yield break;
        // SceneChanger.toTriggerFadeOut = true;
        yield return new WaitForSeconds(1f);
        GameObject.FindGameObjectWithTag("WeaponSound").GetComponent<AudioManager>().Play("GameOver");
        WeaponLibrary.ResetWeapons();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        Score.points = 0;
    }
}