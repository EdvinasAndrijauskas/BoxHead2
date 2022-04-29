using System;
using System.Collections.Generic;
using data;
using DefaultNamespace;
using model;
using model.ammo;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponShooting : MonoBehaviour, IWeaponShooting
{    
    public Transform firePoint; 
    public List<GameObject> projectile;
    public Animator muzzleFlash;
    public Text currentAmmo;
    public Text backupAmmo;
    public Text weaponName;

    float _timeToFire = 0f;
    Weapon _currentWeapon;
    private readonly List<Weapon> _weapons = WeaponLibrary.Weapons;
    int _currentWeaponIndex = 0;
    public UnityEvent<float> reloading;
    private float _currentDelay = 1f;
    private bool _canShoot = true;
    private static readonly int Shoot = Animator.StringToHash("Shoot");

    
    private Coroutine reloadCoroutine;

    private void Start()
    {
        _currentWeapon = FindWeaponById(WeaponId.Pistol.ToString());
        reloading?.Invoke(_currentDelay);
    }


    private void Update()
    {
        if (_currentWeapon.weaponId != WeaponId.Pistol.ToString())
        {
            if (Input.GetButton("Shoot") && Time.time >= _timeToFire)
            {
                StartShooting();
            }
        }
        else
        {
            if (Input.GetButtonDown("Shoot") && Time.time >= _timeToFire)
            {
                StartShooting();
            }
        }
        
        UpdateUI();

        //next gun
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (_currentWeapon.isRealoding)
            {
                StopCoroutine(reloadCoroutine);
                _canShoot = true;
                _currentWeapon.isRealoding = false;
                reloading?.Invoke(1 );
            }
            if (_currentWeaponIndex < _weapons.Count - 1)
            {
                _currentWeaponIndex += 1;
                _currentWeapon = _weapons[_currentWeaponIndex];
                GameObject.Find("Canvas/WeaponBar/Image").GetComponent<WeaponInformation>().UpdateWeaponImage(_currentWeapon.weaponId);

            }
        }
        
        //previous gun
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (_currentWeapon.isRealoding)
            {            
                StopCoroutine(reloadCoroutine);
                _canShoot = true;
                _currentWeapon.isRealoding = false;
                reloading?.Invoke(1 );
            }
            
            if (_currentWeaponIndex > 0)
            {
                _currentWeaponIndex -= 1;
                _currentWeapon = _weapons[_currentWeaponIndex];
                GameObject.Find("Canvas/WeaponBar/Image").GetComponent<WeaponInformation>().UpdateWeaponImage(_currentWeapon.weaponId);
            }
        }

        //reload
        if (Input.GetKeyDown(KeyCode.R))
        { 
            reloadCoroutine = StartCoroutine(_currentWeapon.Reload());
        }

        if (_currentWeapon.isRealoding)
        {
            _canShoot = false;
            _currentDelay -= Time.deltaTime;
            reloading?.Invoke(_currentDelay / _currentWeapon.reloadTime );
            
            if (_currentDelay <= 0)
            {
                _canShoot = true;
                _currentWeapon.isRealoding = false;
                reloading?.Invoke(1 );
            }
        }
    }

    private void UpdateUI()
    {
        string backup = _currentWeapon.weaponId == WeaponId.Pistol.ToString()
            ? "∞"
            : _currentWeapon.remainingBackupAmmo.ToString();
        currentAmmo.text = _currentWeapon.currentMagazineAmmo + "/";
        backupAmmo.fontSize = backup.Equals("∞") ? 60 : 25;
        backupAmmo.text = backup;
        weaponName.text = _currentWeapon.weaponId;
    }

    private void StartShooting()
    {
        if (!_canShoot)
        {
            Invoke(nameof(NoAmmoBlink), 0f);
            Invoke(nameof(RemoveAmmoBlink), 0.1f);
        }

        int totalRemainingAmmo = _currentWeapon.TotalRemainingAmmo();

        if (totalRemainingAmmo != 0 && !_currentWeapon.isRealoding)
        {
            if (_currentWeapon.currentMagazineAmmo == 0)
            {
                reloadCoroutine = StartCoroutine(_currentWeapon.Reload());
            }
            else
            {
                if (_canShoot) _currentDelay = _currentWeapon.reloadTime;
                _timeToFire = Time.time + 1f / _currentWeapon.fireRate;
                FindWeaponShooting(_currentWeapon);
                muzzleFlash.SetTrigger(Shoot);
            }
        }
        
    }

    private void NoAmmoBlink()
    {
        GameObject.FindGameObjectWithTag("BulletBar").GetComponent<Image>().color = Color.red;
    }
    
    private void RemoveAmmoBlink()
    {
        GameObject.FindGameObjectWithTag("BulletBar").GetComponent<Image>().color = Color.white;
    }
    
    Weapon FindWeaponById(String weaponId)
    {
        return _weapons.Find(weapon => weapon.weaponId == weaponId);
    }
    
    GameObject FindProjectile(String name)
    {
        return projectile.Find(o => o.name == name);
    }

    private void FindWeaponShooting(Weapon weapon)
    {
        string weaponId = weapon.weaponId;
        
        if (weaponId == WeaponId.Pistol.ToString() || weaponId == WeaponId.Rifle.ToString() || weaponId == WeaponId.Javelin.ToString() )
        {
            DirectShooting(weapon.ammoType);
        }
        else if (weaponId == WeaponId.Shotgun.ToString() )
        {
            SpreadShooting(weapon.ammoType,2f, 10f);
        }
        else if (weaponId == WeaponId.GrenadeLauncher.ToString() )
        {
            SpreadShooting(weapon.ammoType,3f , 25f);
        }
        else if (weaponId == WeaponId.Flamethrower.ToString())
        {
            FlamethrowerShooting();
        }
        else if (weaponId == WeaponId.Railgun.ToString())
        {
            LaserShooting(weapon.ammoType);
        }
        _currentWeapon.Shoot();
    }

    public void DirectShooting(string ammo)
    {
        GameObject bullet = Instantiate(FindProjectile(ammo), firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * _currentWeapon.bulletForce, ForceMode2D.Impulse);
    }
    
    public void SpreadShooting(string ammo, float  numberOfProjectiles,float spreadAngle )
    {
        float angleStep = spreadAngle / numberOfProjectiles;
        float centeringOffset = (spreadAngle / 2) - (angleStep / 2);
        
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            Vector3 currentBulletAngle = new Vector3(0, 0, angleStep * i - centeringOffset);
            Quaternion rotation = Quaternion.Euler(firePoint.rotation.eulerAngles + currentBulletAngle);

            GameObject bullet = Instantiate(FindProjectile(ammo), firePoint.position, rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(bullet.transform.up * _currentWeapon.bulletForce, ForceMode2D.Impulse);

        }
    }

    public void FlamethrowerShooting()
    {
        Instantiate(FindProjectile("Flame"), firePoint.position, firePoint.rotation);
    }
    
    public void LaserShooting(string ammo)
    {
        var position = firePoint.position;
        RaycastHit2D hit = Physics2D.Raycast(position, firePoint.up,100f);
        GameObject laser = Instantiate(FindProjectile(ammo), position, firePoint.rotation);

        if (hit.collider != null)
        {
            laser.GetComponent<Laser>().SetTargetPosition(hit.point);
        }
        else
        {
            var endPosition = firePoint.position + transform.up * 100f;
            laser.GetComponent<Laser>().SetTargetPosition(endPosition);
        }
    }

}





