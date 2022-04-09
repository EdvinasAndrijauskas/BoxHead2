using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float retreaDistance;
    private float timeBtwShots;
    [SerializeField] private float startTimeBtwShots;

    [SerializeField] private Transform player;
    [SerializeField] private GameObject rightFlame;
    [SerializeField] private GameObject leftFlame;
    private bool _facingRight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Soldier").transform;
        timeBtwShots = startTimeBtwShots;
        _facingRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (player.position.x > transform.position.x &&
                _facingRight) //if the target is to the right of enemy and the enemy is not facing right
                Flip();
            if (player.position.x < transform.position.x && !_facingRight)
                Flip();

            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, player.position) > stoppingDistance &&
                     Vector2.Distance(transform.position, player.position) > retreaDistance)
            {
                transform.position = this.transform.position;
            }
            else if (Vector2.Distance(transform.position, player.position) > retreaDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            }

            if (timeBtwShots <= 0)
            {
                if (!GameObject.FindGameObjectWithTag("Soldier").GetComponent<PlayerHealth>().CurrentHealth.Equals(0))
                {
                    if (_facingRight)
                    {
                        Instantiate(leftFlame, transform.position, Quaternion.identity);
                        timeBtwShots = startTimeBtwShots;
                    }
                    else
                    {
                        Instantiate(rightFlame, transform.position, Quaternion.identity);
                        timeBtwShots = startTimeBtwShots;
                    }
                }
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        _facingRight = !_facingRight;
    }
}