using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject target;
    private float moveSpeed;
    Vector2 directionToTarget;
    //   HealthSystem healthSystem;
    //  public Transform HealthBar;
    private Animator anim;
    // [SerializeField] private float attackSpeed =10f; 
    // [SerializeField] private float attackDamage =10f;
    private float canAttack;
    //[SerializeField] private GameObject explosion;
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Soldier");
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = Random.Range(1f, 3f);
      //  healthSystem = new HealthSystem(100);
      //  Transform healthBarTransform = Instantiate(HealthBar, new Vector2(0, 10), Quaternion.identity);
      //  HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
      //  healthBar.SetUp(healthSystem);
      //  Debug.Log("Health: " + healthSystem.GetHealthPercent());
        anim.SetFloat("Speed", moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector2 direction = target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }
    void FixedUpdate()
    {
        MoveMonster();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.collider.tag)
        {
            case "Soldier":
            //   healthSystem.Damage(50);
            Debug.Log("YOU LOSE");
                    EnemySpawner.spawnAllowed = false;
                anim.SetTrigger("Attack");
                Destroy(col.gameObject);
                target = null;
                break;
            case "Bullet":
                //add score how many zombies killed?
                //healthSystem.Damage(20);
                //Destroy(col.gameObject);
                 Destroy(gameObject);
                //Do damage function
                //EnemySpawner.spawnAllowed = false;
                //Destroy(col.gameObject);
                // target = null;
                break;
        }
    }
    private void MoveMonster()
    {
        if (target != null)
        {
            directionToTarget = (target.transform.position - transform.position).normalized;
            rb.velocity = new Vector2(directionToTarget.x * moveSpeed, directionToTarget.y * moveSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    
}
