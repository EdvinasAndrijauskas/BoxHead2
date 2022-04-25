using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject explosion;
    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag.Equals("Enemy"))
        {
            col.gameObject.GetComponent<ZombieHealth>().Damage(100);
            
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

    private void OnDestroy()
    {
        GameObject missileExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(missileExplosion,0.5f);
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
