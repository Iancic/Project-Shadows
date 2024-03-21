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

    public static PlayerController Instance { get; private set; }
    
    protected List<EnemyController> _enemiesInRange = new List<EnemyController>();

    private void Awake()
    {
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
        playerCamera = Camera.main;

        UpgradesManager.Instance.OnUpgradeChanged += (type, f) =>
        {
            if (type == UpgradeType.MovementSpeed)
            {
                speed = baseSpeed + f;
            }
        };
    }

    protected virtual void Update()
    {
        if (HP <= 0)
        {
            // SceneManager.LoadScene("RestartMenu");
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
            Damage(1);
        }

        if (collision.gameObject.CompareTag("Barrel"))
        {
            Damage(20);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Bulb"))
        {
            Damageable = false;
        }
    }

    public void Damage(int damage)
    {
        HP -= damage;
    }
}