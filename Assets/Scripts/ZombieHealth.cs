using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour, IHealthSystem
{
    public int Health { get; set; }
    public int CurrentHealth { get; set; }
    [SerializeField] private HealthBar HealthBar;

    // Start is called before the first frame update
    void Start()
    {
        Health = 100;
        CurrentHealth = Health;
        HealthBar.SetMaxHealth(Health);
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void Damage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth < 0) CurrentHealth = 0;
        HealthBar.SetHealth(CurrentHealth);
    }
}