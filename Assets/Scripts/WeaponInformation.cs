
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
