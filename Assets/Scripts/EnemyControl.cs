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
    //[SerializeField] private GameObject explosion;
    void Start()
    {
        target = GameObject.FindWithTag("Soldier");
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = Random.Range(1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        MoveMonster();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("PLEASE TRIGGER");
        switch (col.collider.tag)
        {
            case "Soldier": 
                EnemySpawner.spawnAllowed = false;
                Debug.Log("SUPPOSED TGO BE DEAD");
                Destroy(col.gameObject);
                target = null;
                break;
            case "Bullet":
                //add score how many dier?
                Destroy(col.gameObject);
                Destroy(gameObject);
                Debug.Log("SUPPOSED TGO BE DEAD");
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
