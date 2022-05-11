using System.Collections;
using System.Globalization;
using SFX;
using UnityEngine;

namespace model
{
    public class Weapon
    {
        public string WeaponId { get;  }
        private readonly int _maxBackupAmmo;  //immutable
        private readonly int _magazineCapacity; //immutable
        public readonly float BulletForce;
        public readonly float FireRate;
        public int CurrentMagazineAmmo { get; private set; }
        public int RemainingBackupAmmo { get; private set; }
        public float ReloadTime { get;  }
        public bool IsRealoding { get; set; }
        public string AmmoType { get; }
        public bool IsLocked { get; set; }
        public int UnlockLevel { get; }
        
        
        public Weapon(string weaponId, int magazineCapacity,float fireRate, float reloadTime, string ammoType,int unlockLevel, float bulletForce = -1, int maxBackupAmmo = -1)
        {
            this.WeaponId = weaponId;
            this._maxBackupAmmo = maxBackupAmmo;
            this._magazineCapacity = magazineCapacity;
            this.BulletForce = bulletForce;
            this.FireRate = fireRate;
            this.ReloadTime = reloadTime;
            this.AmmoType = ammoType;
            this.UnlockLevel = unlockLevel;

            CurrentMagazineAmmo = magazineCapacity;
            RemainingBackupAmmo = maxBackupAmmo;
            IsRealoding = false;
            IsLocked = weaponId != data.WeaponId.Pistol.ToString();
        }

        public void Shoot()
        {
            CurrentMagazineAmmo--;
        }

        public IEnumerator Reload()
        {
            int totalRemainingAmmo = TotalRemainingAmmo();
            if (totalRemainingAmmo <= 0 && _maxBackupAmmo != -1)
            {
                IsRealoding = false;
                Debug.Log("No Bullets");
                //TODO: add sound
                GameObject.FindGameObjectWithTag("WeaponSound").GetComponent<AudioManager>().Play("EmptyMagazine");
            }
            else
            {
                if (CurrentMagazineAmmo < _magazineCapacity)
                {
                    IsRealoding = true;
                    //TODO: Reload Sound 
                    GameObject.FindGameObjectWithTag("WeaponSound").GetComponent<AudioManager>().Play(ReloadTime.ToString(CultureInfo.InvariantCulture));
                    yield return new WaitForSeconds(ReloadTime);
                }
          
                if (_maxBackupAmmo == -1)
                {
                    CurrentMagazineAmmo = _magazineCapacity;
                }
                else
                {
                    int ammoBalanceInMagazine = _magazineCapacity - CurrentMagazineAmmo;
                    
                    if (ammoBalanceInMagazine >= RemainingBackupAmmo)
                    {
                        CurrentMagazineAmmo += RemainingBackupAmmo;
                        RemainingBackupAmmo = 0;
                    }
                    else
                    {
                        RemainingBackupAmmo -= ammoBalanceInMagazine; 
                        CurrentMagazineAmmo += ammoBalanceInMagazine;
                    }
                }
            }
        }
        public int TotalRemainingAmmo()
        {
            if (WeaponId == data.WeaponId.Pistol.ToString()) return -1;
            return RemainingBackupAmmo + CurrentMagazineAmmo;
        }

        public void RefillAmmo()
        {
            CurrentMagazineAmmo = _magazineCapacity;
            RemainingBackupAmmo = _maxBackupAmmo;
        }

        public bool AmmoUsed()
        {
            if (CurrentMagazineAmmo == _magazineCapacity && RemainingBackupAmmo == _maxBackupAmmo)
            {
                return false;
            }
            
            return true;
        }
    }
}