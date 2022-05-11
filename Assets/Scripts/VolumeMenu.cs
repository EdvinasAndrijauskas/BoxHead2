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
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackButton();
        }
    }

    public void BackButton()
    { 
        PlayerPrefs.SetFloat("VolumeValue", _slider.value);
        _pauseMenuScene.SetActive(true);
        _weaponMenuScene.SetActive(false);
        _levelMenuScene.SetActive(false);
        _OptionMenuScene.SetActive(false);
        LoadValues();
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
