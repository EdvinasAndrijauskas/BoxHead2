using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunShooting : MonoBehaviour
{    
    public Transform firePoint; 
    public GameObject bulletPrefab;
    public float bulletForce = 50f;
    public float fireRate = 5f;
    public Animator muzzleFlash;

    enum Guns
    {
        Pistol,
        Shotgun,
    }
    
    float _timeToFire = 0f;
    Guns _currentGun = Guns.Pistol;
    List<Guns> _guns= new List<Guns>();
    int _currentGunIndex;

    private void Start()
    {
        //TODO: Could be Array 
        //Enum.GetNames(typeof(_guns))
        _guns.Add(Guns.Pistol);
        _currentGunIndex = 0;
    }


    private void Update()
    {
        if (Input.GetButtonDown("Shoot")&& Time.time >= _timeToFire )
        {
            _timeToFire = Time.time + 1f / fireRate;
            GetWeapon(_currentGun);
            muzzleFlash.SetTrigger("Shoot");

        }
        
        //next gun
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (_currentGunIndex < _guns.Count - 1)
            {
                _currentGunIndex += 1;
                _currentGun = _guns[_currentGunIndex];
            }
        }
        
        //previous gun
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (_currentGunIndex > 0)
            {
                _currentGunIndex -= 1;
                _currentGun = _guns[_currentGunIndex];
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            AddGun();
        }
        
    }

    void AddGun()
    {
        _guns.Add(Guns.Shotgun);
    }

    private void GetWeapon(Guns gun)
    {
        switch(gun) 
        {
            case Guns.Pistol:
                PistolShooting();
                break;
            case Guns.Shotgun:
                ShotGunShooting();
                break;
        }
    }
    
    private void PistolShooting()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
    
    private void ShotGunShooting()
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

          //  Debug.Log("HIT SOMEThING : " + hit.collider.name);
        }
        else
        {
            var endPosition = firePoint.position + transform.right * 100f;
            trailScript.SetTargetPosition(endPosition);
        }
        
    }
}





