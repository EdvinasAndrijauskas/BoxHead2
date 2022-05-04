
using System.Collections.Generic;
using data;
using model;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInformation : MonoBehaviour
{
    
    [SerializeField] private Sprite pistol;
    [SerializeField] private Sprite shotgun;
    [SerializeField] private Sprite rifle;
    [SerializeField] private Sprite javelin;
    [SerializeField] private Sprite flamethrower;
    [SerializeField] private Sprite grenadeLauncher;
    [SerializeField] private Sprite railgun;
    [SerializeField] private Text currentAmmoText;
    [SerializeField] private Text backupAmmoText;
    [SerializeField] private Text weaponNameText;
    [SerializeField] private Text unlockText;
    
    public void UpdateWeaponImage(string weaponId)
    {

        Sprite weaponImage = pistol;
        if (weaponId == WeaponId.Pistol.ToString())
        {
            weaponImage = pistol;
        }
        
        if (weaponId == WeaponId.Rifle.ToString())
        {
            weaponImage = rifle;
        }
        
        if (weaponId == WeaponId.Shotgun.ToString())
        {
            weaponImage = shotgun;
        }
        
        if (weaponId == WeaponId.Javelin.ToString())
        {
            weaponImage =  javelin;
        }
        if (weaponId == WeaponId.Flamethrower.ToString())
        {
            weaponImage = flamethrower;
        }
        if (weaponId == WeaponId.GrenadeLauncher.ToString())
        {
            weaponImage = grenadeLauncher;
        }
        if (weaponId == WeaponId.Railgun.ToString())
        {
            weaponImage = railgun;
        }

        GameObject.FindGameObjectWithTag("WeaponImage").GetComponent<Image>().sprite = weaponImage;
    }
    
    public void UpdateWeaponAmmo(Weapon weapon)
    {
        string backup = weapon.weaponId == WeaponId.Pistol.ToString() ? "∞" : weapon.remainingBackupAmmo.ToString();
        currentAmmoText.text = weapon.currentMagazineAmmo + "/";
        backupAmmoText.fontSize = backup.Equals("∞") ? 60 : 25;
        backupAmmoText.text = backup;
        weaponNameText.text = weapon.weaponId;
    }

    public void EnabledText(string weaponName)
    {
        unlockText.enabled = true;
        unlockText.text = weaponName + " Unlocked";
        
        Invoke(nameof(DisableText),3f);
    }

    private void DisableText()
    {
        unlockText.enabled = false;
    }
}
