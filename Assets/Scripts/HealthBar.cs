using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider Slider;

    public void SetMaxHealth(float health)
    {
        Slider.maxValue = health;
        Slider.value = health;
    }

    public void SetHealth(float health)
    {
        Slider.value = health;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
