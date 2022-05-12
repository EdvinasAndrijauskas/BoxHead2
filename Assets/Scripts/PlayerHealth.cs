using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealthSystem

{
    public float Health { get; set; }
    public float CurrentHealth { get; set; }
    [SerializeField] private HealthBar HealthBar;
    public static bool isDead = false;

    // Start is called before the first frame update
    private  void Start()
    {
        Health = 100;
        CurrentHealth = Health;
        HealthBar.SetMaxHealth(Health);
        InvokeRepeating("Heal", 3, 5);
    }

    public void Damage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
            isDead = true;
            StartCoroutine(GameOver.GameOverScene());
        }
        HealthBar.SetHealth(CurrentHealth);
    }
    public void Heal()
    {
        float newHealth = CurrentHealth;
        if (newHealth == CurrentHealth)
        {
            CurrentHealth += 20;
            if (CurrentHealth > 100) CurrentHealth = 100;
            HealthBar.SetHealth(CurrentHealth);
        }
    }
}