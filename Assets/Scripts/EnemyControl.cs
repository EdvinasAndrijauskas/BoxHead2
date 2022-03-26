using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyControl : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb;
    private GameObject target;
    private float moveSpeed;
    Vector2 directionToTarget;
    private Animator anim;
    
    //[SerializeField] private GameObject explosion;
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Soldier");
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = Random.Range(1f, 3f);
        anim.SetFloat("Speed", moveSpeed );

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
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
                Debug.Log("SUPPOSED SOLDER DEADD");

                EnemySpawner.spawnAllowed = false;
                Destroy(col.gameObject);
                target = null;
                break;
            case "Bullet":
                //add score how many dier?
                Destroy(col.gameObject);
                Destroy(gameObject);
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
