using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Components
    public Camera playerCamera;
    private CharacterController controller;
    private Animator animator;
    public AudioSource footSteps;

    // TODO: Movement speed upgrade
    //Movement Speed
    private float speed = 5f;

    //Player Logic
    public int hitPoints = 100;
    [HideInInspector] public bool isImmune;

    //XP
    public int currentXP = 0, maxXP = 10;
    public int level = 1;

    //Battery & Ammo
    [HideInInspector] public float batteryMax = 60.00f, batteryCurrent = 20.00f;

    [HideInInspector] public int currentAmmo = 6, maxAmmo = 6;

    // TODO: Reload upgrade
    public float reloadTime = 6f;
    public bool isReloading = false;

    public MeshRenderer gun;
    public bool isCarrying;

    public bool canShoot = false;

    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
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

    void Update()
    {
        if (hitPoints < 0)
        {
            SceneManager.LoadScene("Restart");
        }

        //Ammo
        if (currentAmmo <= 0)
        {
            isReloading = true;
            UIManager.Instance.ToggleReloadIcon(true);
        }

        if (isReloading)
        {
            StartCoroutine(ReloadGun());
        }

        //Battery
        if (batteryCurrent > batteryMax)
        {
            batteryCurrent = batteryMax;
            //Don't let the battery exceed the limit
        }

        //Carrying
        if (isCarrying)
        {
            gun.enabled = false;
        }
        else
        {
            gun.enabled = true;
        }

        //Flashlight
        if (Flashlight.Instance.isOn == true)
            batteryCurrent -= Time.deltaTime;

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
            hitPoints -= 1;
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
        isReloading = false;
        canShoot = false;
        yield return new WaitForSeconds(reloadTime);
        UIManager.Instance.ToggleReloadIcon(false);
        currentAmmo = maxAmmo;
        canShoot = true;
    }
}