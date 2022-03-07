using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // [SerializeField]
    // private GameObject hitEffect;
    private void OnCollisionEnter2D(Collision2D collision)
    {
      //  GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
       // Destroy(effect,2f);
        Destroy(gameObject);
    }
    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
