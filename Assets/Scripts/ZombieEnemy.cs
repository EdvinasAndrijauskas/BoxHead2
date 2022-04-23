using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject target;
    private float moveSpeed;
    private Vector2 directionToTarget;
    private Animator anim;
    private PlayerHealth PlayerHealth;

    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Soldier");
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = Random.Range(1f, 3f);
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
        if (col.collider.tag.Equals("Soldier"))
        {
            anim.SetTrigger("Attack");
            //Maybe variable in here?
            col.gameObject.GetComponent<PlayerHealth>().Damage(20);
            /*Debug.Log(col.gameObject.GetComponent<PlayerHealth>().CurrentHealth +
                      "->>>>>>>>>>>>>>>>>>>>> PLAYER DAMAGED TAKEN");*/
            if (col.gameObject.GetComponent<PlayerHealth>().CurrentHealth.Equals(0))
            {
                target = null;
                EnemySpawner.spawnAllowed = false;
                col.gameObject.GetComponent<PlayerMovement>().enabled = false;
                col.gameObject.GetComponent<Animator>().SetTrigger("isDeadByZombie");
                Destroy(col.gameObject, 1.5f);
            }
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