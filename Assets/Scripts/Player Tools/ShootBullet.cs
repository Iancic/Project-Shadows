using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform bulletSpawnPoint; // The point from which the bullet will be instantiated
    public float bulletSpeed = 20f; // Speed of the bullet

    public int maxAmmo = 16, currentAmmo;

    public int reloadTime = 3;

    public ParticleSystem muzzle;

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
    }

    private void Start()
    {
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
            // Check if the left mouse button is pressed to shoot
            if (Input.GetMouseButtonDown(0) && currentAmmo > 0)
            {
                currentAmmo = currentAmmo - 1;
                // Instantiate the bullet
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
