using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthSystem
{
    int Health { get; set; }
    int CurrentHealth { get; set; }
    void Damage(int damage);
}
