using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    // Shooting
    private bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 0.2f;

    // Burst
    public int bulletsPerBurst = 3;
    private int burstBulletsLeft;

    // Spread
    public float spreadIntensity;

    // Bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 50;
    public float bulletPrefabLife = 3f;

    public GameObject muzzleEffect;
    private Animator animator;

    // Loading
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;

    public int weaponDamage = 20; // Default damage for pistol

    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    public enum WeaponModel
    {
        M1911,
        AK74,
        M4,
        Uzi
    }

    public WeaponModel thisWeaponModel;

    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();

        bulletsLeft = magazineSize;
    }

    void Update()
    {
        if (bulletsLeft <= 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            SoundManager.Instance.emptyMagazineSoundM1911.Play();
        }

        // Input handling for different shooting modes
        switch (currentShootingMode)
        {
            case ShootingMode.Single:
                if (Input.GetKeyDown(KeyCode.Mouse0) && readyToShoot && bulletsLeft > 0)
                {
                    FireWeapon();
                }
                break;

            case ShootingMode.Burst:
                if (Input.GetKeyDown(KeyCode.Mouse0) && readyToShoot && bulletsLeft > 0)
                {
                    burstBulletsLeft = bulletsPerBurst; // Reset burst bullets left
                    FireWeapon();
                }
                break;

            case ShootingMode.Auto:
                if (Input.GetKey(KeyCode.Mouse0) && readyToShoot && bulletsLeft > 0)
                {
                    FireWeapon();
                }
                break;
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && isReloading == false)
        {
            Reload();
        }

        // Automatically reload when magazine is empty
        if (readyToShoot && !isShooting && !isReloading && bulletsLeft <= 0)
        {
            //Reload();
        }

        if (AmmoManager.Instance.ammoDisplay != null)
        {
            AmmoManager.Instance.ammoDisplay.text = $"{bulletsLeft}/{magazineSize}";
        }
    }

    private void FireWeapon()
    {
        bulletsLeft--;

        muzzleEffect.GetComponent<ParticleSystem>().Play();
        animator.SetTrigger("RECOIL");

        SoundManager.Instance.PlayShootingSound(thisWeaponModel);

        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        // Instantiate bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        // Set the bullet's damage based on the weapon
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDamage(weaponDamage);
        }

        // Pointing the bullet to face the shooting direction
        bullet.transform.forward = shootingDirection;

        // Shoot bullet
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

        // Destroy bullet after some time
        StartCoroutine(DestoryBulletAfterTime(bullet, bulletPrefabLife));

        // Set up next shot timing
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        // Burst Mode Logic
        if (currentShootingMode == ShootingMode.Burst)
        {
            if (burstBulletsLeft > 1)
            {
                burstBulletsLeft--;
                Invoke("FireWeapon", shootingDelay);
            }
        }
    }

    private void Reload()
    {
        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);

        SoundManager.Instance.PlayReloadSound(thisWeaponModel);
    }

    private void ReloadCompleted()
    {
        bulletsLeft = magazineSize;
        isReloading = false;
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        // Shooting from the middle of the screen to check where are pointing at
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            // Hitting Something
            targetPoint = hit.point;
        }
        else
        {
            // Shooting at the air
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        // Returning the shooting direction and spread
        return direction + new Vector3(x, y, 0);
    }

    private IEnumerator DestoryBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
