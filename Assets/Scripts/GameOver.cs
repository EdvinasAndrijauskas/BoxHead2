using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    
    public void QuitGame()
        {
            Debug.Log ("QUIT!");
            Application.Quit();
        }

    public void RestartGame()
    {
        SceneManager.LoadScene("SabinaSceneNew");
        SceneChanger.toTriggerFadeOut = false;
        PlayerHealth.isDead = false;
        Debug.Log("RESTART");
    }

    public static IEnumerator GameOverScene()
    {
        if (!PlayerHealth.isDead) yield break;
        SceneChanger.toTriggerFadeOut = true;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}
