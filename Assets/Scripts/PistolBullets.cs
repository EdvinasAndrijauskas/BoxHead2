using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PistolBullets : MonoBehaviour
{
    private ZombieHealth ZombieHealth;
 
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag.Equals("Enemy"))
        {
            col.gameObject.GetComponent<ZombieHealth>().Damage(25);
            Debug.Log(col.gameObject.GetComponent<ZombieHealth>().CurrentHealth + "->>>>>>>>>>>>>>>>>>>>> ZOMBIE DAMAGED TAKEN");
            if (col.gameObject.GetComponent<ZombieHealth>().CurrentHealth.Equals(0))
            {
                Destroy(col.gameObject);
            }
        }
        Destroy(gameObject);
    }
    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
