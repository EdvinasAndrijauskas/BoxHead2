using SFX;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    
    // Start is called before the first frame update
    void Start()
    {
        _slider.onValueChanged.AddListener(val => AudioManager.instance.ChangeMasterVolume(val));
    }
}
