using System;
using System.Collections.Generic;
using data;
using model;
using UnityEngine;
using Random = UnityEngine.Random;

public class SupplyBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Soldier"))
        {
            Destroy(gameObject);
        }
    }

    /*void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag.Equals("Soldier"))
        {
            SpawnSupplyBox();
        }
    }*/
}
