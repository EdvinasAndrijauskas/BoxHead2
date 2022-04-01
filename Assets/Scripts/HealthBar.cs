using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private HealthSystem _healthSystem; 
    // Start is called before the first frame update
    public void SetUp(HealthSystem healthSystem)
    {
        this._healthSystem = healthSystem;
        
        _healthSystem.OnHealthChanged += HealthSystemOnOnHealthChanged;
    }

    private void HealthSystemOnOnHealthChanged(object sender, EventArgs e)
    {
        transform.Find("HealthBar").localScale = new Vector3(_healthSystem.GetHealthPercent(), 1);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Find("HealthBar").localScale = new Vector3(_healthSystem.GetHealthPercent(), 1);
    }
}
