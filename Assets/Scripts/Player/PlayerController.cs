using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    //Components
    public Camera playerCamera;
    private CharacterController controller;
    private Animator animator;
    public AudioSource footSteps;
    public Flashlight Flashlight;

    //Movement Speed
    private float baseSpeed = 5f;
    private float speed = 0f;

    //Player Logic
    [HideInInspector] public int HP = 100;
    [HideInInspector] public bool Damageable;

    //XP
    public int CurrentXP = 0;
    public int MaxXP = 10;
    public int Level = 1;

    //Ammo
    [HideInInspector] public int currentAmmo = 6, maxAmmo = 6;

    public float reloadTime = 6f;
    public bool isReloading = false;

    [HideInInspector] public bool canShoot = true;

    public static PlayerController Instance { get; private set; }
    
    protected List<EnemyController> _enemiesInRange = new List<EnemyController>();
    private float _reloadTimeUpgrade = 0f;

    private void Awake()
    {
        currentAmmo = maxAmmo;
        Damageable = false;

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        footSteps = gameObject.GetComponent<AudioSource>();
        controller = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        Flashlight = GetComponentInChildren<Flashlight>();
        speed = baseSpeed + UpgradesManager.Instance.GetValue(UpgradeType.MovementSpeed);
        _reloadTimeUpgrade = UpgradesManager.Instance.GetValue(UpgradeType.ReloadTime);
        maxAmmo = (int) UpgradesManager.Instance.GetValue(UpgradeType.MaxAmmo);
        playerCamera = Camera.main;
        footSteps = GetComponent<AudioSource>();

        UpgradesManager.Instance.OnUpgradeChanged += (type, f) =>
        {
            if (type == UpgradeType.MovementSpeed)
            {
                speed = baseSpeed + f;
            }

            if (type == UpgradeType.ReloadTime)
            {
                _reloadTimeUpgrade = f;
            }

            if (type == UpgradeType.MaxAmmo)
            {
                maxAmmo = (int) f;
            }
        };
    }

    protected virtual void Update()
    {
        if (HP <= 0)
        {
            // SceneManager.LoadScene("Restart");
        }

        //Ammo
        if (currentAmmo <= 0 && isReloading == false)
        {
            isReloading = true;
            UIManager.Instance.ToggleReloadIcon(true);
        }

        if (isReloading)
        {
            StartCoroutine(ReloadGun());
        }

        //Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Create a Vector3 for movement only if there is input to process
        if (horizontalInput != 0 || verticalInput != 0)
        {
            Vector3 move = new Vector3(horizontalInput, 0, verticalInput);
            controller.Move(move * Time.deltaTime * speed);
            animator.SetBool("isRunning", true);
            footSteps.enabled = true;
        }
        else
        {
            footSteps.enabled = false;
            animator.SetBool("isRunning", false);
        }

        // Handle Orientation
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = hit.point;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
        }

        if (CurrentXP == MaxXP)
        {
            CurrentXP = 0;
            Level++;
        }

        _enemiesInRange = Flashlight.Logic();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Bulb"))
        {
            if (collision.gameObject.GetComponent<Bulb>().isOn == true)
                Damageable = true;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            HP -= 1; //Enemy.DAMAGE
        }

        if (collision.gameObject.CompareTag("Barrel"))
        {
            HP -= 10; //Barrel.DAMAGE
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Bulb"))
        {
            Damageable = false;
        }
    }

    public IEnumerator ReloadGun()
    {
        canShoot = false;
        yield return new WaitForSeconds(reloadTime + _reloadTimeUpgrade); // + because the upgrade is a negative value
        UIManager.Instance.ToggleReloadIcon(false);
        currentAmmo = maxAmmo;
        isReloading = false;
        canShoot = true;
    }
}