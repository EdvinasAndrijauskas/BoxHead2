using System.Collections;
using System.Collections.Generic;
using data;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInformation : MonoBehaviour
{
    
    public Sprite pistol;
    public Sprite shotgun;
    public void UpdateWeaponImage(string weaponId)
    {
        if (weaponId == WeaponId.Pistol.ToString())
        {
            GetComponent<Image>().sprite = pistol;
        }
        
        if (weaponId == WeaponId.Shotgun.ToString())
        {
            GetComponent<Image>().sprite = shotgun;
        }
    }
    
}
