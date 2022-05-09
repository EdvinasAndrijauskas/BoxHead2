using UnityEngine;

public class VolumeMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuScene;
    [SerializeField] private GameObject _weaponMenuScene;
    [SerializeField] private GameObject _levelMenuScene;
    [SerializeField] private GameObject _OptionMenuScene;
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackButton();
        }
    }
    // Update is called once per frame
    public void setVolume()
    { 
        
    }
    
    public void BackButton()
    { 
        _pauseMenuScene.SetActive(true);
        _weaponMenuScene.SetActive(false);
        _levelMenuScene.SetActive(false);
        _OptionMenuScene.SetActive(false);
    }
}
