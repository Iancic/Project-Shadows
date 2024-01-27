using System.Collections;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    //Components
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public ParticleSystem muzzle;
    public AudioSource bulletSound;

    //Gun Logic
    public float bulletSpeed = 20f;
    public int maxAmmo = 16, currentAmmo;
    public int reloadTime = 3;

    //Tool Selection
    public bool isSelected = true;

    public static ShootBullet Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        bulletSound = gameObject.GetComponent<AudioSource>();
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }

        if (isSelected)
        {
            if (Input.GetMouseButtonDown(0) && currentAmmo > 0)
            {
                bulletSound.Play();

                currentAmmo = currentAmmo - 1;
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                muzzle.Play();

                StartCoroutine(CameraController.Instance.Shake(0.3f, 0.6f));

                // Add velocity to the bullet
                if (bullet.TryGetComponent<Rigidbody>(out Rigidbody rb))
                {
                    rb.velocity = bulletSpawnPoint.forward * bulletSpeed;
                }
            }
        }
    }

    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
    }
}
