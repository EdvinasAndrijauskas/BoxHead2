using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IShootingRayCast
{
    private void GetDamage(RaycastHit2D hit2D)
   {
        gameObject.SetActive(false);
   }


   public void ReceiveDamage(RaycastHit2D hit2D)
   {
       GetDamage(hit2D);
   }
}
