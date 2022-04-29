using System.Collections;
using System.Collections.Generic;
using data;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInformation : MonoBehaviour
{
    
    public Sprite pistol;
    public Sprite shotgun;
    public Sprite rifle;
    public Sprite javelin;
    public Sprite flamethrower;
    public Sprite grenadeLauncher;
    public Sprite railgun;

    public void UpdateWeaponImage(string weaponId)
    {
        if (weaponId == WeaponId.Pistol.ToString())
        {
            GetComponent<Image>().sprite = pistol;
        }
        
        if (weaponId == WeaponId.Rifle.ToString())
        {
            GetComponent<Image>().sprite = rifle;
        }
        
        if (weaponId == WeaponId.Shotgun.ToString())
        {
            GetComponent<Image>().sprite = shotgun;
        }
        
        if (weaponId == WeaponId.Javelin.ToString())
        {
            GetComponent<Image>().sprite = javelin;
        }
        if (weaponId == WeaponId.Flamethrower.ToString())
        {
            GetComponent<Image>().sprite = flamethrower;
        }
        if (weaponId == WeaponId.GrenadeLauncher.ToString())
        {
            GetComponent<Image>().sprite = grenadeLauncher;
        }
        if (weaponId == WeaponId.Railgun.ToString())
        {
            GetComponent<Image>().sprite = railgun;
        }
    }
    
}
