using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesBullets : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer ("Ignore Raycast");
    }

    // Start is called before the first frame update
    private Transform _player;
    private Vector2 _target;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Soldier").transform;
        _target = new Vector2(_player.position.x, _player.position.y);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target, speed * Time.deltaTime);
        if (transform.position.x == _target.x && transform.position.y == _target.y)
        {
           Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag.Equals("Soldier"))
        {
            col.gameObject.GetComponent<PlayerHealth>().Damage(30);
            if (col.gameObject.GetComponent<PlayerHealth>().CurrentHealth.Equals(0))
            {
                //THIS ONE IS FOR OLD Spawner
               // EnemySpawner.spawnAllowed = false;
                col.gameObject.GetComponent<Animator>().SetTrigger("isDeadByWizard");
                col.gameObject.GetComponent<PlayerMovement>().enabled = false;
                Destroy(col.gameObject, 1.5f);
            }
        }
    }
}