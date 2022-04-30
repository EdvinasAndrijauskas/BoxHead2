using System.Collections.Generic;
using model;

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

    public class WeaponLibrary
    {
        public static List<Weapon> Weapons = new List<Weapon>
        {
            new Weapon(WeaponId.Pistol.ToString(),7,  5f,  1f,Ammo.Bullet.ToString(),50f),
            new Weapon(WeaponId.Rifle.ToString(),30 , 10f,1.5f,Ammo.Bullet.ToString(),50f,60),
            new Weapon(WeaponId.Shotgun.ToString(),12, 5f,2f, Ammo.Bullet.ToString(),75f,24),
            new Weapon(WeaponId.GrenadeLauncher.ToString(),4, 1f,2.5f,Ammo.EMP_Grenade.ToString(),50f,12 ),
            new Weapon(WeaponId.Javelin.ToString(),1, 1f,1f,Ammo.Missile.ToString(),100f ,2),
            new Weapon(WeaponId.Flamethrower.ToString(),28, 1f,3f,Ammo.Flame.ToString() ,maxBackupAmmo: 28),
            new Weapon(WeaponId.Railgun.ToString(),12, 1f,2f,Ammo.Laser.ToString(),maxBackupAmmo: 12 ),
        };
    }
}