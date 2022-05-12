using SFX;
using UnityEngine;
using UnityEngine.UI;

public class VolumeMenu : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _pauseMenuScene;
    [SerializeField] private GameObject _weaponMenuScene;
    [SerializeField] private GameObject _levelMenuScene;
    [SerializeField] private GameObject _OptionMenuScene;

    private void Start()
    {
        VolumeSlider();
        PauseGame.inOptions = true;
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackButtonPause();
        }
    }

    public void BackButtonPause()
    { 
        PlayerPrefs.SetFloat("VolumeValue", _slider.value);
        _pauseMenuScene.SetActive(true);
        _weaponMenuScene.SetActive(false);
        _levelMenuScene.SetActive(false);
        _OptionMenuScene.SetActive(false);
        LoadValues();
        PauseGame.inOptions = false;
    }

    public void VolumeSlider()
    {
        _slider.onValueChanged.AddListener(val => AudioManager.instance.ChangeMasterVolume(val));
    }
    
    void LoadValues()
    {
        float volume = PlayerPrefs.GetFloat("VolumeValue");
        _slider.value = volume;
        AudioListener.volume = volume;
    }
    
}
