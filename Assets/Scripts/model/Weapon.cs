using System.Collections;
using data;
using UnityEngine;

namespace model
{
    public class Weapon
    {
        public string weaponId { get; set; }
        private int maxBackupAmmo;  //immutable
        private int magazineCapacity; //immutable
        public float bulletForce;
        public float fireRate;
        public int currentMagazineAmmo { get; set; }
        public int remainingBackupAmmo { get; set; }
        public float reloadTime { get; set; }
        public bool isRealoding { get; set; }
        public string ammoType { get; set; }
        public bool isLocked { get; set; }
        public int unlockLevel { get; set; }
        
        
        public Weapon(string weaponId, int magazineCapacity,float fireRate, float reloadTime, string ammoType,int unlockLevel, float bulletForce = -1, int maxBackupAmmo = -1)
        {
            this.weaponId = weaponId;
            this.maxBackupAmmo = maxBackupAmmo;
            this.magazineCapacity = magazineCapacity;
            this.bulletForce = bulletForce;
            this.fireRate = fireRate;
            this.reloadTime = reloadTime;
            this.ammoType = ammoType;
            this.unlockLevel = unlockLevel;

            currentMagazineAmmo = magazineCapacity;
            remainingBackupAmmo = maxBackupAmmo;
            isRealoding = false;
            isLocked = weaponId != WeaponId.Pistol.ToString();
        }

        public void Shoot()
        {
            currentMagazineAmmo--;
        }

        public IEnumerator Reload()
        {
            int totalRemainingAmmo = TotalRemainingAmmo();
            if (totalRemainingAmmo <= 0 && maxBackupAmmo != -1)
            {
                isRealoding = false;
                Debug.Log("No Bullets");
                //TODO: add sound
            }
            else
            {
                if (currentMagazineAmmo < magazineCapacity)
                {
                    isRealoding = true;
                    yield return new WaitForSeconds(reloadTime);
                }
          
                if (maxBackupAmmo == -1)
                {
                    currentMagazineAmmo = magazineCapacity;
                }
                else
                {
                    int ammoBalanceInMagazine = magazineCapacity - currentMagazineAmmo;
                    
                    if (ammoBalanceInMagazine >= remainingBackupAmmo)
                    {
                        currentMagazineAmmo += remainingBackupAmmo;
                        remainingBackupAmmo = 0;
                    }
                    else
                    {
                        remainingBackupAmmo -= ammoBalanceInMagazine; 
                        currentMagazineAmmo += ammoBalanceInMagazine;
                    }
                }
            }
        }
        public int TotalRemainingAmmo()
        {
            if (weaponId == WeaponId.Pistol.ToString()) return -1;
            return remainingBackupAmmo + currentMagazineAmmo;
        }
    }
}