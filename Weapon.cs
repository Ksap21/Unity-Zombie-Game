using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


//Weapon's  character
public class Weapon : MonoBehaviour
{

    public Camera playerCamera;

    //Shooting
    public bool isShooting, readyToShoot;

    
    bool allowReset = true;
    public float shootingDelay = 0.3f;
    public int weaponDamage;

    ////Burst mode

    public int bulletsPerBurst = 3;
    public int burstBulletleft;

  //  Sprite for UI display

    public Sprite weaponSprite;

    ////Spread mode

    public float spreadIntensity;

    //bullet

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30f;
    public float bulletPrefabLifeTime = 3f;



//muzzle effect and gun animator
    public GameObject muzzleEffect;
    private Animator animator;


   // Reloading
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;

    public enum WeaponModel
    {
        Pistol,
        Rifle
    }


    public WeaponModel thisWeaponModel;
    
    public enum ShootingMode
    {
        Auto,
        Single,
        Burst
        
    }

    public ShootingMode currentShootingMode;
    

    private void Awake()
    {
       
        readyToShoot = true;
        burstBulletleft = bulletsPerBurst;

        bulletsLeft = magazineSize;
        animator = GetComponent<Animator>();
   
    }

    // Update is called once per frame
    void Update()
    {
        //shoots automatic 

        if (bulletsLeft == 0 && isShooting)
        {
            SoundManager.Instance.emptyMagazineRifle.Play();
        }

        if (currentShootingMode == ShootingMode.Auto)
       
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);
      

        }

        //burst and single mode
        else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
        {
      
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }

        //Reloading
        if (Input.GetKeyDown(KeyCode.Mouse1) && bulletsLeft < magazineSize && isReloading == false)
        {
            Reload();
        }

       // UI Reloading


        // if (readyToShoot && isShooting)
        //{
        //    burstBulletleft = bulletsPerBurst;
        //    FireWeapon();

        //}

        //shoots if all passes true and bullets left in magazine is greater than 0
        if (readyToShoot && isShooting && bulletsLeft > 0)
        {
            burstBulletleft = bulletsPerBurst;
            FireWeapon();

        }

        


    }
    private void FireWeapon()

    {
       
        muzzleEffect.GetComponent<ParticleSystem>().Play();
        animator.SetTrigger("RECOIL");

        


        bulletsLeft--;

        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;
        //instantiate the bullet
        GameObject Bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        Bullet bul = Bullet.GetComponent<Bullet>();
        bul.bulletDamage = weaponDamage;

        //pointing bullet to shooting direction

        Bullet.transform.forward = shootingDirection;

        //shoot the bullet
        Bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized * bulletVelocity, ForceMode.Impulse);

        //Destroy the bullet after some time
        StartCoroutine(DestroyBulletAfterTime(Bullet, bulletPrefabLifeTime));

        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        //burstMode
        if (currentShootingMode == ShootingMode.Burst && burstBulletleft > 1)
        {
            burstBulletleft--;
            Invoke("FireWeapon", shootingDelay);
        }

       if(thisWeaponModel == WeaponModel.Pistol)
        {
            SoundManager.Instance.shootingSoundPistol.PlayOneShot(SoundManager.Instance.PistolFire);
        }
       else if(thisWeaponModel == WeaponModel.Rifle)
        {
            SoundManager.Instance.shootingSoundRifle.PlayOneShot(SoundManager.Instance.RifleFire);
        }
    }

    private void Reload()
    {

        if (!isShooting)
        {
            SoundManager.Instance.reloadingSoundRifle.PlayOneShot(SoundManager.Instance.RifleReload);
       
            animator.SetTrigger("RELOAD");
            
            isReloading = true;
            Invoke("ReloadCompleted", reloadTime);
        }
    }

    private void ReloadCompleted()
    {
        bulletsLeft = magazineSize;
        isReloading = false;

     //   AmmoBarManager.Instance.UpdateAmmoUI();
    }


    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    public bool isAimingDownSights = false;  // Flag for ADS (Aiming Down Sights)

    public Vector3 CalculateDirectionAndSpread()
    {
        // Shooting from the middle of the screen to check where we are pointing
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;

        // Check if the ray hits something
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;  // The point where the ray hit
        }
        else
        {
            targetPoint = ray.GetPoint(100);  // Default point if ray doesn't hit anything
        }

        // Calculate the direction to the target point
        Vector3 direction = targetPoint - bulletSpawn.position;

        // Apply spread to the direction
        float currentSpread = isAimingDownSights ? spreadIntensity / 2 : spreadIntensity;  // Adjust spread if aiming down sights
        float x = UnityEngine.Random.Range(-currentSpread / 2, currentSpread / 2);
        float y = UnityEngine.Random.Range(-currentSpread / 2, currentSpread / 2);

        // Combine direction with the spread and normalize it
        Vector3 spreadVector = new Vector3(x, y, 0);
        Vector3 finalDirection = (direction + spreadVector).normalized;

        return finalDirection;
    }


    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}

