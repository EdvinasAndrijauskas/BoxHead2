using System;
using System.Collections.Generic;
using System.Linq;
using data;
using model;
using UnityEngine;
using UnityEngine.UI;

public class GunShooting : MonoBehaviour
{    
    public Transform firePoint; 
    public GameObject bulletPrefab;
    public float bulletForce = 50f;
    public float fireRate = 5f;
    public Animator muzzleFlash;
    public Text ammoInfo;
    
    float _timeToFire = 0f;
    Weapon _currentWeapon;
    private List<Weapon> weapons = WeaponLibrary.Weapons;
    int _currentWeaponIndex = 0;

    private void Start()
    {
        //TODO: Could be Array 
        //Enum.GetNames(typeof(_guns))
        _currentWeapon = FindWeaponById(WeaponId.Pistol.ToString());
    }


    private void Update()
    {
        if (Input.GetButtonDown("Shoot")&& Time.time >= _timeToFire )
        {
            if (_currentWeapon.remainingAmmo != 0)
            {
                _timeToFire = Time.time + 1f / fireRate;
                WeaponShooting(_currentWeapon.weaponId);
                muzzleFlash.SetTrigger("Shoot");
            }
        }
        
        //next gun
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (_currentWeaponIndex < weapons.Count - 1)
            {
                _currentWeaponIndex += 1;
                _currentWeapon = weapons[_currentWeaponIndex];
            }
        }
        
        //previous gun
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (_currentWeaponIndex > 0)
            {
                _currentWeaponIndex -= 1;
                _currentWeapon = weapons[_currentWeaponIndex];
            }
        }

        //reload
        if (Input.GetKeyDown(KeyCode.R))
        {
           //StartCoroutine(_currentWeapon.Reload());
        }

        ammoInfo.text = "Bullet: " + _currentWeapon.currentAmmo + " / " +  _currentWeapon.weaponId == WeaponId.Pistol.ToString()  ?   "∞"  :  _currentWeapon.currentAmmo.ToString();

    }
    

    Weapon FindWeaponById(String weaponId)
    {
        return weapons.Find(weapon => weapon.weaponId == weaponId);
    }

    private void WeaponShooting(String weaponId)
    {
      
            if (weaponId == WeaponId.Pistol.ToString())
            {
                PistolShooting();
            }
            else if (weaponId == WeaponId.Shotgun.ToString())
            {
                ShotGunShooting();
            }

            /*switch(weaponId) 
            {
                case WeaponId.Pistol:
                    PistolShooting();
                    break;
                case WeaponId.Shotgun:
                    ShotGunShooting();
                    break;
            }*/
            _currentWeapon.Shoot();
        


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

            Debug.Log("HIT SOMEThING : " + hit.collider.name);
        }
        else
        {
            var endPosition = firePoint.position + transform.right * 100f;
            trailScript.SetTargetPosition(endPosition);
        }
        
    }
}





