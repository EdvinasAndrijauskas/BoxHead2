using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour, IHealthSystem
{
    public float Health { get; set; }
    public float CurrentHealth { get; set; }
    [SerializeField] private HealthBar HealthBar;

    // Start is called before the first frame update
    private void Start()
    {
        Health = 100;
        CurrentHealth = Health;
        HealthBar.SetMaxHealth(Health);
    }

    public void Damage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth < 0) CurrentHealth = 0;
        HealthBar.SetHealth(CurrentHealth);
    }
}