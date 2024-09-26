using System.Collections;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //Shooting
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    //Burst
    public int bulletsPerBurst = 3;
    public int burstBulletLeft;

    //Spread
    public float spreadIntensity;

    // Bullet
    public GameObject bulletPrefab;
    public Transform bulletSpwan;
    public float bulletVelocity = 30;
    public float bulletPrefabLifeTime = 3f;

    public GameObject muzzleEffect;
    private Animator animator;

    //Reloading
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;


    public enum WeaponModel
    {
        Pistol,
        M416,

    }

    public WeaponModel thisWeaponModel;

    public enum ShootingMode
    {
        Single,
        Brust,
        Auto
    }

    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();
        bulletsLeft = magazineSize;
    }

    void Update()
    {
        if (bulletsLeft == 0 && isShooting)
        {
            SoundManager.Instance.emptyMagzineSoundPistol.Play();
        }

        if (currentShootingMode == ShootingMode.Auto)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Brust)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !isReloading)
        {
            Reload();
        }

        if (readyToShoot && !isShooting && !isReloading && bulletsLeft <= 0)
        {
            // Reload();
        }

        if (readyToShoot && isShooting && bulletsLeft > 0)
        {
            burstBulletLeft = bulletsPerBurst;
            FireWeapon();
        }

        UpdateAmmoDisplay();
    }

    private void FireWeapon()
    {
        bulletsLeft--;
        UpdateAmmoDisplay();

        muzzleEffect.GetComponent<ParticleSystem>().Play();
        animator.SetTrigger("RECOIL");

        // SoundManager.Instance.shootingSoundPistol.Play();

        SoundManager.Instance.PlayShootingSound(thisWeaponModel);


        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpwan.position, Quaternion.identity);
        bullet.transform.forward = shootingDirection;
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));

        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        if (currentShootingMode == ShootingMode.Brust && burstBulletLeft > 1)
        {
            burstBulletLeft--;
            Invoke("FireWeapon", shootingDelay);
        }
    }

    private void Reload()
    {

        //SoundManager.Instance.reloadingSoundPistol.Play();
        SoundManager.Instance.PlayReloadSound(thisWeaponModel);

        animator.SetTrigger("RELOAD");

        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);
    }

    private void ReloadCompleted()
    {
        bulletsLeft = magazineSize;
        isReloading = false;
        UpdateAmmoDisplay();
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            // Hitting something
            targetPoint = hit.point;
        }
        else
        {
            // Shooting in air
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpwan.position;

        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        return direction + new Vector3(x, y, 0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    private void UpdateAmmoDisplay()
    {
        if (AmmoManager.Instance != null && AmmoManager.Instance.ammoDispaly != null)
        {
            AmmoManager.Instance.ammoDispaly.text = $"{bulletsLeft}/{magazineSize}";
        }
    }
}
