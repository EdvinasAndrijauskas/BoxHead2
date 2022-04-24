using System.Collections;
using UnityEngine;

namespace model
{
    public class Weapon
    {
        public string weaponId { get; set; }
        private int maxBackupAmmo { get; set; } //immutable
        private int magazineCapacity { get; set; } //immutable
        public int currentMagazineAmmo { get; set; }
        public int remainingBackupAmmo { get; set; }
        public float reloadTime { get; set; }
        
        
        public Weapon(string weaponId, int magazineCapacity, float reloadTime, int maxBackupAmmo = -1)
        {
            this.weaponId = weaponId;
            this.maxBackupAmmo = maxBackupAmmo;
            this.magazineCapacity = magazineCapacity;
            this.reloadTime = reloadTime;
            
            currentMagazineAmmo = magazineCapacity;
            remainingBackupAmmo = maxBackupAmmo;
        }

        public void Shoot()
        {
            currentMagazineAmmo--;
            if (currentMagazineAmmo % maxBackupAmmo == 0)
            {
                Reload();
            }
        }

        public IEnumerator Reload()
        {
            int totalRemainingAmmo = TotalRemainingAmmo();
            if (totalRemainingAmmo <= 0 && maxBackupAmmo != -1)
            {
                Debug.Log("No Bullets");
                //TODO: add sound
            }
            else
            {

                if (currentMagazineAmmo < magazineCapacity)
                {
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
            return remainingBackupAmmo + currentMagazineAmmo;
        }
    }
}