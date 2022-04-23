using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag.Equals("Enemy"))
        {
            col.gameObject.GetComponent<ZombieHealth>().Damage(25);
            //Debug.Log(col.gameObject.GetComponent<ZombieHealth>().CurrentHealth + "->>>>>>>>>>>>>>>>>>>>> ZOMBIE DAMAGED TAKEN");
            if (col.gameObject.GetComponent<ZombieHealth>().CurrentHealth.Equals(0))
            {
                Destroy(col.gameObject);
            }
        }
        if (!col.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
    
    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
