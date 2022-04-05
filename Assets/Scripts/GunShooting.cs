using UnityEngine;

public class GunShooting : MonoBehaviour
{    
    [SerializeField] private Transform firePoint; 
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletForce = 20f;
    
    //TODO : Additional feature for other guns
    public float fireRate = 15f;
    private float _timeToFire = 0f;
    
    private void Update()
    {
        if (Input.GetButton("Shoot") && Time.time >= _timeToFire)
        {
            _timeToFire = Time.time + 1f / fireRate;
            ShootGunShooting();
            
        }
    }
    
    private void PistolShooting()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
    
    private void ShootGunShooting()
    {
        float numberOfProjectiles = 3;
        float spreadAngle = 20f;
        
        float angleStep = spreadAngle / numberOfProjectiles;
        float aimingAngle = firePoint.rotation.eulerAngles.z;
        float centeringOffset = (spreadAngle / 2) - (angleStep / 2);                                                                                                                         //centered on the mouse cursor
  
        for (int i = 0; i < 3; i++)
        {
            float currentBulletAngle = angleStep * i;
            Quaternion rotation = Quaternion.Euler(new Vector3(0,0,aimingAngle + currentBulletAngle - centeringOffset));

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(bullet.transform.right * bulletForce, ForceMode2D.Impulse);
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





