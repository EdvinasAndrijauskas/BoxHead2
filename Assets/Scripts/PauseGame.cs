using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
   public static bool GameIsPaused = false;
   [SerializeField] private GameObject _pauseMenuCanvas;
   [SerializeField] private GameObject _weaponMenuCanvas;
   [SerializeField] private GameObject _levelMenuCanvas;
   [SerializeField] private GameObject _OptionMenuScene;
   [SerializeField] private GameObject _SceneChangerCanvas;
   
   // Update is called once per frame
   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
         if (GameIsPaused)
         {

            Resume();
         }
         else
         {
            Pause();
         }
      }
   }

   public void Resume()
   { 
      _pauseMenuCanvas.SetActive(false);
      _weaponMenuCanvas.SetActive(true);
      _levelMenuCanvas.SetActive(true);
      Time.timeScale = 1f;
      GameIsPaused = false;
   }

   private void Pause()
   {
       _pauseMenuCanvas.SetActive(true);
      _weaponMenuCanvas.SetActive(false);
      _levelMenuCanvas.SetActive(false);
      SceneChanger.toTriggerFadeOut = false;
      Time.timeScale = 0f;
      GameIsPaused = true;
   }

   public void QuitGame()
   {
      Debug.Log ("QUIT!");
      Application.Quit();
   }

   public void SetVolume()
   {
      Debug.Log("volume");
   }
}