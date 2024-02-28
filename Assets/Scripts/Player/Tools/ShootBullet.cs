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

    //Tool Selection
    public bool isSelected = true;

    public GameObject flashObject; //LIGHT 
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
    }

    void Update()
    {

        if (isSelected)
        {
            if (Input.GetMouseButtonDown(0) && PlayerController.Instance.currentAmmo > 0 && PlayerController.Instance.canShoot == true)
            {
                StartCoroutine(Flash());
                bulletSound.Play();

                PlayerController.Instance.currentAmmo = PlayerController.Instance.currentAmmo - 1;
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

    public IEnumerator Flash()
    {
        flashObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        flashObject.SetActive(false);
    }
}
