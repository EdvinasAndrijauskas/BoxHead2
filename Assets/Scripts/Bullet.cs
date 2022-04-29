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
        if (col.collider.tag.Equals("Zombie"))
        {
            col.gameObject.GetComponent<ZombieHealth>().Damage(25);
            if (col.gameObject.GetComponent<ZombieHealth>().CurrentHealth.Equals(0))
            {
                Destroy(col.gameObject);
            }
        }
        if (col.collider.tag.Equals("Wizard"))
        {
            col.gameObject.GetComponent<Wizardhealth>().Damage(20);
            if (col.gameObject.GetComponent<Wizardhealth>().CurrentHealth.Equals(0))
            {
                Destroy(col.gameObject);
            }
        }
        if (!col.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
    
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
