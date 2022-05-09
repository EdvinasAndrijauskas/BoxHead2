using UnityEngine;
using UnityEngine.Audio;

public class VolumeMenu : MonoBehaviour
{
    public AudioMixer _AudioMixer;
    [SerializeField] private GameObject _pauseMenuScene;
    [SerializeField] private GameObject _weaponMenuScene;
    [SerializeField] private GameObject _levelMenuScene;
    [SerializeField] private GameObject _OptionMenuScene;
   
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackButton();
        }
    }
    public void setVolume(float volume)
    {
        _AudioMixer.SetFloat("volume", volume);
    }
    
    public void BackButton()
    { 
        _pauseMenuScene.SetActive(true);
        _weaponMenuScene.SetActive(false);
        _levelMenuScene.SetActive(false);
        _OptionMenuScene.SetActive(false);
    }
}
