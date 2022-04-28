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
        Destroy(gameObject);
       OnDestroy();
    }
    
    private void OnDestroy()
    {
        GameObject missileExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(missileExplosion.transform.position, 25);
        foreach (Collider2D collider2D in colliders)
        {
            if (collider2D.tag.Equals("Enemy"))
            {
                collider2D.gameObject.GetComponent<ZombieHealth>().Damage(25);
                
                if (collider2D.gameObject.GetComponent<ZombieHealth>().CurrentHealth.Equals(0))
                {
                    Destroy(collider2D.gameObject);
                }
            }
            
        }
        
        Destroy(missileExplosion,0.5f);
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
