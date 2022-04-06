using UnityEngine;

public class GunShooting : MonoBehaviour
{    
    [SerializeField] private Transform firePoint; 
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletForce = 50f;
    [SerializeField] private float fireRate = 5f;
    [SerializeField] private Animator muzzleFlash;

    //TODO : Additional feature for other guns
    private float _timeToFire = 0f;
    
    private void Update()
    {
        if (Input.GetButtonDown("Shoot")&& Time.time >= _timeToFire )
        {
            _timeToFire = Time.time + 1f / fireRate;
            PistolShooting();
            muzzleFlash.SetTrigger("Shoot");

        }
    }
    
    private void PistolShooting()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
    
    private void ShootGunShooting()
    {
        
        float numberOfProjectiles = 3;
        float spreadAngle = 25f;
        float angleStep = spreadAngle / numberOfProjectiles;
        float centeringOffset = (spreadAngle / 2) - (angleStep / 2);
        
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            Vector3 currentBulletAngle = new Vector3(0, 0, angleStep * i - centeringOffset);
            Quaternion rotation = Quaternion.Euler(firePoint.rotation.eulerAngles + currentBulletAngle);

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(bullet.transform.up * bulletForce, ForceMode2D.Impulse);

        }
    }
    
    //TODO: In progress
    void LaserShooting()
    {
        //Use debug with gizmo
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * 10f, Color.red);
        var position = firePoint.position;
        RaycastHit2D hit = Physics2D.Raycast(position, transform.right,100f);
            
        var trail = Instantiate(bulletPrefab, position, firePoint.rotation);
        var trailScript = trail.GetComponent<BulletTrail>();
            
        if (hit.collider != null)
        {
            trailScript.SetTargetPosition(hit.point);
            var hittable = hit.collider.GetComponent<IShootingRayCast>();
            hittable?.ReceiveDamage(hit);

            Debug.Log("HIT SOMEThING : " + hit.collider.name);
        }
        else
        {
            var endPosition = firePoint.position + transform.right * 100f;
            trailScript.SetTargetPosition(endPosition);
        }
        
    }
}





