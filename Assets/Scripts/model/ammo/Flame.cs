
using System;
using System.Security.Cryptography;
using UnityEngine;

public class Flame : MonoBehaviour
{
    private Transform firePoint;
    private Animator animator;
    private static Flame flame;
    private float horizontalInput;
    private Vector3 rotate;
    private float verticalInput;
    private void Awake()
    {
        if (flame == null)
        {
            flame = this;
            
        }
        else
        {
            Destroy(gameObject);

        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        firePoint = GameObject.FindGameObjectWithTag("FirePoint").GetComponent<Transform>();
        rotate = new Vector3(0,0,90 );
        transform.Rotate(rotate);

    }
    
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, firePoint.position, 100 * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(firePoint.rotation.eulerAngles  + rotate) ,
            250 * Time.deltaTime);

        if (Input.GetButtonUp("Shoot"))
        {
            animator.Play("Flamethrower End");
            animator.fireEvents = false;
            Destroy(gameObject,0.25f);
        }
    }

    

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag.Equals("Enemy"))
        {
            col.gameObject.GetComponent<ZombieHealth>().Damage(1);
            
            if (col.gameObject.GetComponent<ZombieHealth>().CurrentHealth.Equals(0))
            {
                Destroy(col.gameObject);
            }
        }    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
