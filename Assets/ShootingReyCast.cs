using UnityEngine;
using Debug = UnityEngine.Debug;
using Vector2 = UnityEngine.Vector2;

public class ShootingReyCast : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    public float bulletForce = 20f;
    public float range = 100f;
    public float timeToDestroy = 3f;
    public int speed = 100;
    public float fireRate = 15f;

    private float timeToFire = 0f;
    void Update()
    {
        if (Input.GetButton("Shoot") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1f / fireRate;
            Shoot();
            
        }
        
    }

    void Shoot()
    {
        //Use debug with gizmo
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * 10f, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), range);
        if (hit)
        {
            Debug.Log("HIT SOMETING : " + hit.collider.name);
            hit.transform.GetComponent<SpriteRenderer>().color = Color.red;
        }

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
 