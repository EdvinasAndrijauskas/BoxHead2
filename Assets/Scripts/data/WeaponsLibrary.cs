using System.Collections.Generic;
using model;
using UnityEngine;
using weapon;

namespace data
{
    enum WeaponId
    {
        Pistol,
        Rifle,
        Shotgun,
        Railgun,
        Javelin,
        Flamethrower,
        GrenadeLauncher,
    }

    public static class WeaponLibrary 
    {
        public static List<Weapon> Weapons = new List<Weapon>
        {
            new Weapon(WeaponId.Pistol.ToString(),7,  5f,  1f,Ammo.Bullet.ToString(),1,50f),
            new Weapon(WeaponId.Shotgun.ToString(),12, 2.5f,1.5f, Ammo.Bullet.ToString(),2,75f,24),
            new Weapon(WeaponId.Rifle.ToString(),30 , 10f,1.5f,Ammo.Bullet.ToString(),4,50f,60),
            new Weapon(WeaponId.GrenadeLauncher.ToString(),4, 1f,1.5f,Ammo.EMP_Grenade.ToString(),6,50f,12 ),
            new Weapon(WeaponId.Flamethrower.ToString(),28, 3f,2f,Ammo.Flame.ToString() ,8,maxBackupAmmo: 28),
            new Weapon(WeaponId.Javelin.ToString(),1, 1f,2f,Ammo.Missile.ToString(),10,100f ,2),
            new Weapon(WeaponId.Railgun.ToString(),12, 2f,2f,Ammo.Laser.ToString(),12,maxBackupAmmo: 12 ),
        };
        
        
        public static void UnlockWeapon(int level)
        {
            for (int i = 1; i < Weapons.Count; i++)
            {
                if (Weapons[i].UnlockLevel == level)
                {
                    GameObject.Find("WeaponInformation").GetComponent<WeaponInformation>().EnabledText(Weapons[i].WeaponId);
                    
                    Debug.Log(Weapons[i].WeaponId + " UNLOCKED");
                    Weapons[i].IsLocked = false;
                    return;
                }
            }
        }

        public static void ResetWeapons()
        {
            foreach (var weapon in Weapons)
            {
                if (weapon.WeaponId != WeaponId.Pistol.ToString())
                {
                    weapon.IsLocked = true;
                }
                weapon.RefillAmmo();
            }
        }
    }
}