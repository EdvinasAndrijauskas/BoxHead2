using System.Collections.Generic;
using data;
using model;
using SFX;
using UnityEngine;
using Random = System.Random;

namespace supplybox
{
    public class SupplyBox : MonoBehaviour
    {
        [SerializeField] private GameObject supplyBoxGameObject;

        private void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer ("Ignore Raycast");
        }

        private string RefillAmmo(List<Weapon> weapons)
        {
            List<Weapon> unlockedWeapons = new List<Weapon>();
            for (int i = 1; i < weapons.Count; i++)
            {
                if (!weapons[i].IsLocked)
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
                return "All Ammo is full";
            }
            Debug.Log(unlockedWeapons[index].WeaponId);
            unlockedWeapons[index].RefillAmmo();
            return unlockedWeapons[index].WeaponId;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag.Equals("Soldier"))    
            {
                GameObject supplyBox = Instantiate(supplyBoxGameObject,transform.position, Quaternion.identity);
                GameObject.FindGameObjectWithTag("WeaponSound").GetComponent<AudioManager>().Play("SupplyBox");

                SupplyBoxText supplyBoxText = supplyBox.GetComponent<SupplyBoxText>();
                supplyBoxText.Setup(RefillAmmo(WeaponLibrary.Weapons));
            
                Destroy(gameObject);
            }
        }

    }
}
