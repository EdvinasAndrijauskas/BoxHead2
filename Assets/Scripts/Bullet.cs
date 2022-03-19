using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
