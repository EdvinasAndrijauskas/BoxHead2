using System.Collections.Generic;
using model;

namespace data
{
    enum WeaponId
    {
        Pistol,
        Rifle,
        Shotgun,
        Sniper,
        Javelin,
        Flamethrower,
        GrenadeLauncher,
    }
    
    public class WeaponLibrary
    {
        public static List<Weapon> Weapons = new List<Weapon>
        {
            new Weapon(WeaponId.Pistol.ToString(),7, 50f, 5f,  1f),
            new Weapon(WeaponId.Rifle.ToString(),30 ,50f, 10f,1.5f,60),
            new Weapon(WeaponId.Shotgun.ToString(),12,75f, 5f,2f, 24),
            new Weapon(WeaponId.Javelin.ToString(),10,100f, 2f,1f ),
            new Weapon(WeaponId.Flamethrower.ToString(),100,100f, 2f,1f ),
            new Weapon(WeaponId.GrenadeLauncher.ToString(),100,50f, 2f,1f ),
            new Weapon(WeaponId.Sniper.ToString(),100,50f, 2f,1f )

        };
    }
}