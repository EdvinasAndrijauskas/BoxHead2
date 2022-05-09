using UnityEngine;

public class PauseGame : MonoBehaviour
{
   public static bool GameIsPaused = false;
   [SerializeField] private GameObject _pauseMenuScene;
   [SerializeField] private GameObject _weaponMenuScene;
   [SerializeField] private GameObject _levelMenuScene;
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
      _pauseMenuScene.SetActive(false);
      _weaponMenuScene.SetActive(true);
      _levelMenuScene.SetActive(true);
      Time.timeScale = 1f;
      GameIsPaused = false;
   }

   private void Pause()
   {
       _pauseMenuScene.SetActive(true);
      _weaponMenuScene.SetActive(false);
      _levelMenuScene.SetActive(false);
      SceneChanger.toTriggerFadeOut = false;
      Time.timeScale = 0f;
      GameIsPaused = true;
   }

   public void QuitGame()
   {
      Debug.Log ("QUIT!");
      Application.Quit();
   }
}