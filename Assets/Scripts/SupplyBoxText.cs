using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SupplyBoxText : MonoBehaviour
{
    private TextMeshPro _textMeshPro;
    private float disappearTime;
    private Color textColor;

    private void Awake()
    {
        _textMeshPro = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(string supplyText)
    {
        string ammo = "Ammo";
        _textMeshPro.text = supplyText + " " + ammo;
        textColor = _textMeshPro.color;
        disappearTime = 0.3f;
        
    }

    private void Update()
    {
        transform.position += new Vector3(0, 15 ) * Time.deltaTime;
        disappearTime -= Time.deltaTime;
        if (disappearTime < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            _textMeshPro.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);

            }
        }
    }
    
    
}

