using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class GunController : MonoBehaviour
{
    //Components
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public ParticleSystem muzzle;
    public AudioSource bulletSound;

    //Gun Logic
    public float bulletSpeed = 20f;

    //Ammo
    [HideInInspector] public int currentAmmo = 6;
    [HideInInspector] public int maxAmmo = 6;

    public float reloadTime = 6f;
    private float _reloadTimeUpgrade = 0f;
    public bool isReloading = false;

    [HideInInspector] public bool CanShoot = true;

    public GameObject flashObject; //LIGHT 
    public static GunController Instance { get; private set; }

    private void Start()
    {
        currentAmmo = maxAmmo;

        maxAmmo = (int)UpgradesManager.Instance.GetValue(UpgradeType.MaxAmmo);

        UpgradesManager.Instance.OnUpgradeChanged += (type, f) =>
        {
            if (type == UpgradeType.ReloadTime)
            {
                _reloadTimeUpgrade = f;
            }

            if (type == UpgradeType.MaxAmmo)
            {
                maxAmmo = (int)f;
            }
        };

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        bulletSound = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        //Ammo
        if (currentAmmo <= 0 && isReloading == false && CanShoot == true)
        {
            isReloading = true;
        }

        else if (isReloading)
        {
            isReloading = false;
            StartCoroutine(ReloadGun());
        }


        if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && CanShoot == true && !EventSystem.current.IsPointerOverGameObject())
            {
                StartCoroutine(Flash());
                bulletSound.Play();

                currentAmmo = currentAmmo - 1;
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                muzzle.Play();

                StartCoroutine(CameraController.Instance.Shake(0.6f, 1f));

                // Add velocity to the bullet
                if (bullet.TryGetComponent<Rigidbody>(out Rigidbody rb))
                {
                    rb.velocity = bulletSpawnPoint.forward * bulletSpeed;
                }
            }
    }

    public IEnumerator Flash()
    {
        flashObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        flashObject.SetActive(false);
    }

    public IEnumerator ReloadGun()
    {
        UIManager.Instance.ToggleReloadIcon(true);
        CanShoot = false;
        yield return new WaitForSeconds(reloadTime + _reloadTimeUpgrade); // + because the upgrade is a negative value

        UIManager.Instance.ToggleReloadIcon(false);
        currentAmmo = maxAmmo;
        CanShoot = true;
    }
}
