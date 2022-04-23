﻿using System.Collections.Generic;
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
            new Weapon(WeaponId.Pistol.ToString(),7, 0.5f),
            new Weapon(WeaponId.Shotgun.ToString(),2,1f, maxAmmo:4 )
        };
    }
}