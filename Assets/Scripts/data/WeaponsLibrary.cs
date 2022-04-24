using System.Collections.Generic;
using model;

namespace data
{
    enum WeaponId
    {
        Pistol,
        Shotgun,
    }
    
    public class WeaponLibrary
    {
        public static List<Weapon> Weapons = new List<Weapon>
        {
            new Weapon(WeaponId.Pistol.ToString(),7, 1f),
            new Weapon(WeaponId.Shotgun.ToString(),12,2f, maxBackupAmmo:24)
        };
    }
}