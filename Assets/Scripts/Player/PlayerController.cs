using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Components
    public Camera playerCamera;
    private CharacterController controller;
    private Animator animator;
    public AudioSource footSteps;

    // TODO: Movement speed upgrade
    //Movement Speed
    private float baseSpeed = 5f;

    private float speed = 0f;

    //Player Logic
    [HideInInspector] public int hitPoints = 100;
    [HideInInspector] public bool isImmune;

    //XP
    public int currentXP = 0, maxXP = 10;
    public int level = 1;

    //Ammo
    [HideInInspector] public int currentAmmo = 6, maxAmmo = 6;

    // TODO: Reload upgrade
    public float reloadTime = 6f;
    public bool isReloading = false;

    [HideInInspector] public bool canShoot = true;

    public static PlayerController Instance { get; private set; }

    private float _reloadTimeUpgrade = 0f;
    private float _maxAmmoUpgrade = 0f;

    private void Awake()
    {
        currentAmmo = maxAmmo;
        isImmune = false;

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

    private void Start()
    {
        speed = baseSpeed + UpgradesManager.Instance.GetValue(UpgradeType.MovementSpeed);
        _reloadTimeUpgrade = UpgradesManager.Instance.GetValue(UpgradeType.ReloadTime);
        _maxAmmoUpgrade = UpgradesManager.Instance.GetValue(UpgradeType.MaxAmmo);
        
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

    void Update()
    {
        if (hitPoints <= 0)
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

        if (currentXP == maxXP)
        {
            currentXP = 0;
            level++;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Bulb"))
        {
            if (collision.gameObject.GetComponent<Bulb>().isOn == true)
                isImmune = true;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            hitPoints -= 1; //Enemy.DAMAGE
        }

        if (collision.gameObject.CompareTag("Barrel"))
        {
            hitPoints -= 10; //Barrel.DAMAGE
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Bulb"))
        {
            isImmune = false;
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