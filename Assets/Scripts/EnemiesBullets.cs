using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesBullets : MonoBehaviour
{
    [SerializeField] private float speed;

    // Start is called before the first frame update
    private Transform _player;
    private Vector2 _target;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Soldier").transform;
        _target = new Vector2(_player.position.x, _player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target, speed * Time.deltaTime);
        if (transform.position.x == _target.x && transform.position.y == _target.y)
        {
           Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Soldier"))
        {
            Destroy(gameObject);
        }
    }
}