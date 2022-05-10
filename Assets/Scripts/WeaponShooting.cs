using System;
using System.Collections.Generic;
using data;
using DefaultNamespace;
using model;
using model.ammo;
using SFX;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponShooting : MonoBehaviour, IWeaponShooting
{    
    [SerializeField] private Transform firePoint; 
    [SerializeField] private List<GameObject> projectile;
    [SerializeField] private Animator muzzleFlash;
    [SerializeField] private UnityEvent<float> reloading;
    
    private float _timeToFire = 0f;
    private Weapon _currentWeapon;
    private readonly List<Weapon> _weapons = WeaponLibrary.Weapons;
    private int _currentWeaponIndex = 0;
    private float _currentDelay = 1f;
    private bool _canShoot = true;
    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private Coroutine _reloadCoroutine;

    private void Start()
    {
        _currentWeapon = FindWeaponById(WeaponId.Pistol.ToString());
        reloading?.Invoke(_currentDelay);
    }


    private void Update()
    {
        UpdateUI(_currentWeapon);
        bool input =_currentWeapon.weaponId != WeaponId.Pistol.ToString()  ? Input.GetButton("Shoot") : Input.GetButtonDown("Shoot");
      
        if (input && Time.time >= _timeToFire)
        {
            StartShooting();
        }
        
        //next gun
        if(Input.GetKeyDown(KeyCode.E))
        {
            IsRealoding();

            // Check if weapon is locked
            int nextWeaponIndex = _currentWeaponIndex;
            if (nextWeaponIndex + 1 < _weapons.Count && !_weapons[++nextWeaponIndex].isLocked)
            {
                if (_currentWeaponIndex < _weapons.Count - 1) 
                {
                    _currentWeaponIndex += 1;
                    _currentWeapon = _weapons[_currentWeaponIndex];
                    GameObject.Find("WeaponInformation").GetComponent<WeaponInformation>().UpdateWeaponImage(_currentWeapon.weaponId);
                }
            }
            
        }
        
        //previous gun
        if(Input.GetKeyDown(KeyCode.Q))
        {
            IsRealoding();
            
            if (_currentWeaponIndex > 0)
            {
                _currentWeaponIndex -= 1;
                _currentWeapon = _weapons[_currentWeaponIndex];
                GameObject.Find("WeaponInformation").GetComponent<WeaponInformation>().UpdateWeaponImage(_currentWeapon.weaponId);
            }
        }

        //reload
        if (Input.GetKeyDown(KeyCode.R))
        { 
            _reloadCoroutine = StartCoroutine(_currentWeapon.Reload());
        }
        
        if (_currentWeapon.isRealoding)
        {
            _canShoot = false;
            _currentDelay -= Time.deltaTime;
            reloading?.Invoke(_currentDelay / _currentWeapon.reloadTime );
            
            StopFlame();
            if (_currentDelay <= 0)
            {
                _canShoot = true;
                _currentWeapon.isRealoding = false;
                reloading?.Invoke(1 );
            }
        }
    }

    private void IsRealoding()
    {
        if (_currentWeapon.isRealoding)
        {
            StopCoroutine(_reloadCoroutine);
            _canShoot = true;
            _currentWeapon.isRealoding = false;
            reloading?.Invoke(1);
        }
    }

    private void UpdateUI(Weapon weapon)
    {
        GameObject.Find("WeaponInformation").GetComponent<WeaponInformation>().UpdateWeaponAmmo(weapon);
    }

    private void StartShooting()
    {
        if (!_canShoot)
        {
            Invoke(nameof(ReloadingBlink), 0f);
            Invoke(nameof(RemoveReloadingBlink), 0.1f);
        }

        int totalRemainingAmmo = _currentWeapon.TotalRemainingAmmo();

        if (totalRemainingAmmo != 0 && !_currentWeapon.isRealoding)
        {
            if (_currentWeapon.currentMagazineAmmo == 0)
            {
                _reloadCoroutine = StartCoroutine(_currentWeapon.Reload());
                StopFlame();
            }
            else
            {
                if (_canShoot) _currentDelay = _currentWeapon.reloadTime;
                _timeToFire = Time.time + 1f / _currentWeapon.fireRate;
                FindWeaponShooting(_currentWeapon);
                
                if (_currentWeapon.weaponId != WeaponId.Railgun.ToString()) {
                    muzzleFlash.SetTrigger(Shoot);
                }

            }
        }
        else
        {
            GameObject.FindGameObjectWithTag("WeaponSound").GetComponent<AudioManager>().Play("EmptyMagazine");
            StopFlame();
        }
        
    }

    private void StopFlame()
    {
        if (_currentWeapon.weaponId == WeaponId.Flamethrower.ToString() && GameObject.FindGameObjectWithTag("Flame") != null)
        {
            Flame flame =  GameObject.FindGameObjectWithTag("Flame").GetComponent<Flame>();
            flame.EndFlame();
        }
    }

    private void ReloadingBlink()
    {
        GameObject.FindGameObjectWithTag("BulletBar").GetComponent<Image>().color = Color.red;
    }
    
    private void RemoveReloadingBlink()
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
            FlamethrowerShooting(weapon.ammoType);
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

    public void FlamethrowerShooting(String ammo)
    {
        Instantiate(FindProjectile(ammo), firePoint.position, firePoint.rotation);
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





