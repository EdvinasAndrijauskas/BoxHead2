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
            new Weapon(WeaponId.Pistol.ToString(),7, 50f, 5f,  1f,Ammo.Bullet.ToString()),
            new Weapon(WeaponId.Rifle.ToString(),30 ,50f, 10f,1.5f,Ammo.Bullet.ToString(),60),
            new Weapon(WeaponId.Shotgun.ToString(),12,75f, 5f,2f, Ammo.Bullet.ToString(),24),
            new Weapon(WeaponId.GrenadeLauncher.ToString(),100,75f, 2f,1f,Ammo.EMP_Grenade.ToString() ),
            new Weapon(WeaponId.Javelin.ToString(),10,100f, 2f,1f,Ammo.Missile.ToString() ),
            new Weapon(WeaponId.Flamethrower.ToString(),100,100f, 2f,1f,Ammo.Flame.ToString() ),
            new Weapon(WeaponId.Railgun.ToString(),100,50f, 2f,1f,Ammo.Laser.ToString() ),

        };
    }
}