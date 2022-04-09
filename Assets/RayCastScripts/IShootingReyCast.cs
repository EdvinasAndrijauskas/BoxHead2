using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootingRayCast
{
   void ReceiveDamage(RaycastHit2D hit2D);
}
