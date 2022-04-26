using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject explosion;
    public float timeToExplode;
    private float countdown;
    
    private void Start()
    {
        countdown = timeToExplode;
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 360 * Time.deltaTime));
        Stop(0.75f);

        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            Explode();
            Destroy(gameObject);
        }
    }

    private void Stop(float stop)
    {
        Destroy(gameObject.GetComponent<Rigidbody2D>(),stop);
        Destroy(gameObject.GetComponent<BoxCollider2D>(),stop);
    }

    private void Explode()
    {
        GameObject explosionEffect = Instantiate(explosion, transform.position, Quaternion.identity);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionEffect.transform.position, 8);
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
        Destroy(explosionEffect,0.5f);
        Destroy(gameObject);
    }

    void OnBecameInvisible() {
        Stop(0.0f);
    }
}
