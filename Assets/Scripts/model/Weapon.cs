using System;
using System.Collections;
using UnityEngine;

namespace model
{
    public class Weapon
    {
        public string weaponId { get; set; }
        private int maxAmmo { get; set; } //immutable
        private int magazineCapacity { get; set; } //immutable
        private float reloadTime { get; set; } //immutable
        public int currentAmmo { get; set; }
        public int remainingAmmo { get; set; }
        
        public Weapon(string weaponId, int magazineCapacity, float reloadTime, int maxAmmo = -1)
        {
            this.weaponId = weaponId;
            this.maxAmmo = maxAmmo;
            this.magazineCapacity = magazineCapacity;
            this.reloadTime = reloadTime;
            
            currentAmmo = magazineCapacity;
            remainingAmmo = maxAmmo;
        }

        public void Shoot()
        {
            currentAmmo--;
            if (currentAmmo % maxAmmo == 0 && currentAmmo == 0)
            { 
                Reload();
            }
        }

        public void Reload()
        {
            if (remainingAmmo <= 0 && maxAmmo != -1)
            {
                Debug.Log("No Bullets");
            }
            else
            {
                if (maxAmmo == -1)
                {
                    currentAmmo = magazineCapacity;
                }
                else
                {
                    int ammoBalance = magazineCapacity - currentAmmo;
                    currentAmmo += ammoBalance;
                    remainingAmmo -= ammoBalance;
                }
                Debug.Log("Reloading...");
                //yield return new WaitForSeconds(reloadTime);
            }
        }
    }
}