using System;
using System.Collections.Generic;
using data;
using model;
using UnityEngine;
using Random = System.Random;

public class SupplyBox : MonoBehaviour
{
    private void RefillAmmo(List<Weapon> weapons)
    {
        List<Weapon> unlockedWeapons = new List<Weapon>();
        for (int i = 1; i < weapons.Count; i++)
        {
            if (!weapons[i].isLocked)
            {
                unlockedWeapons.Add(weapons[i]);
            }
        }
        
        var random = new Random();
        int index = random.Next(unlockedWeapons.Count);
        Debug.Log(unlockedWeapons[index].weaponId);

        if (unlockedWeapons[index].AmmoUsed())
        {
            unlockedWeapons[index].RefillAmmo();
        }
        else
        {
            RefillHealth();
        }
    }

    private void RefillHealth()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Soldier"))
        {
            RefillAmmo(WeaponLibrary.Weapons);
            Destroy(gameObject);
        }
    }

}
