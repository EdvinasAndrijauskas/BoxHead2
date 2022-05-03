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
                if (weapons[i].AmmoUsed())
                {
                    unlockedWeapons.Add(weapons[i]);
                }
                else
                {
                    Debug.Log("Ammo is full");
                }
            }
        }
        
        var random = new Random();
        int index = random.Next(unlockedWeapons.Count);
        if (unlockedWeapons.Count == 0)
        {
            Debug.Log("All Ammo is full");
        }
        else
        {
            Debug.Log(unlockedWeapons[index].weaponId);
            unlockedWeapons[index].RefillAmmo();
        }
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
