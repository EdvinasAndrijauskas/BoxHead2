using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class ShootingReyCast : MonoBehaviour
{
    [SerializeField]
    Transform firePoint;
    [SerializeField]
    GameObject bulletPrefab;
    public float bulletForce = 20f;
    public float range = 100f; 
    public float timeToDestroy = 3f;

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetButtonDown("Shoot"))
        {
           Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

           //Use debug with gizmo
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * 10f, Color.red);
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up),range);
            if (hit)
            {
                Debug.Log("HIT SOMETING : " + hit.collider.name);
                hit.transform.GetComponent<SpriteRenderer>().color = Color.red;
            }

            /*Vector2 hitPosition = hit.point;
            ShootBullet(hitPosition);*/
        }
    }

    /*void ShootBullet(Vector2 hitPosition)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        
        LineRenderer line = bullet.GetComponent<LineRenderer>();
        if (line != null)
        {
            line.SetPosition(0,firePoint.position);
            line.SetPosition(1,hitPosition);

        }

        Destroy(bullet, 3f);
    }*/
}
