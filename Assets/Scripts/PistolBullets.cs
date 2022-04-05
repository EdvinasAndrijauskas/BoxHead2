using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PistolBullets : MonoBehaviour
{
 private void OnCollisionEnter2D(Collision2D other)
 {
  if (!other.gameObject.CompareTag("Bullet"))
  {
   Destroy(gameObject);
  }
 }
}
