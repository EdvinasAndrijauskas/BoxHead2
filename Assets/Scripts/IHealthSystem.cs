using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthSystem
{
    float Health { get; set; }
    float CurrentHealth { get; set; }
    void Damage(float damage);
}
