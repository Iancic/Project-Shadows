using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerController : MonoBehaviour
{
    //Components
    public Camera playerCamera;
    private CharacterController controller;
    private Animator animator;
    public AudioSource footSteps;

    //Movement Speed
    private float speed = 5f;

    //Player Logic
    public int hitPoints = 10;
    [HideInInspector] public bool isImmune = false;

    //XP
    public int currentXP = 0, maxXP = 10;
    public int level = 1;

    //Battery
    public float batteryMax = 20.00f, batteryCurrent = 20.00f;
    public int currentAmmo = 3;

    public MeshRenderer organs;
    public MeshRenderer gun;
    public bool isCarrying;

    public static PlayerController Instance { get; private set; }

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

        footSteps = gameObject.GetComponent<AudioSource>();
        controller = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        //Carrying
        if (isCarrying)
        {
            gun.enabled = false;
            organs.enabled = true;
        }
        else
        {
            gun.enabled = true;
            organs.enabled = false;
        }

        if (Flashlight.Instance.isOn == true)
            batteryCurrent -= Time.deltaTime;

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
        if (collision.gameObject.CompareTag("Battery") && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(collision.gameObject);
            batteryCurrent += 1.5f;
        }

        if (collision.gameObject.CompareTag("Bulb"))
        {
            if (collision.gameObject.GetComponent<Bulb>().isOn == true)
                isImmune = true;
        }

        if (collision.gameObject.CompareTag("Ammo") && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Bulb"))
        {
            isImmune = false;
        }
    }
}
