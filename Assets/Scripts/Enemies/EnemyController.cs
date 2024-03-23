using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    //Components
    [Header("Components")] public Transform player;
    public ParticleSystem blood;
    private Animator zombieAnimator;
    public GameObject guts;
    public AudioSource breathing;
    public BoxCollider enemyCollider;
    public Image healthBarImage;
    public GameObject ui;

    [Space(10)] [Header("Enemy Stats")]
    //Movement Speed
    public float BaseSpeed;

    public float RotationSpeed;
    public float AttackSpeed;
    public float AttackDamage;
    public float MaxHP;

    //Zombie Values (Class value means the value of the enemy, multiplier is the value of the multipler at death)
    public int ClassValue = 100;
    public int ClassMultiplier = 1;


    [Header("Behaviour logic")]
    //Logic
    public float HP;

    public bool Alive = true;
    public float Speed = 4.5f;

    public bool IsStunned = false;

    // Represents whenever the enemy is discovered by the DetectionCone of the player's gun
    public bool Seen = false;

    private void Start()
    {
        HP = MaxHP;

        player = PlayerController.Instance.transform;
        enemyCollider = gameObject.GetComponent<BoxCollider>();
        zombieAnimator = gameObject.GetComponent<Animator>();

        //Audio Initialization
        breathing = gameObject.GetComponent<AudioSource>();
        breathing.pitch = Random.Range(0.6f, 1f);
        breathing.Play();
    }
    
    protected virtual void Update()
    {
        if (!Alive) return;
        
        float fillAmount = HP / MaxHP;
        healthBarImage.fillAmount = fillAmount;

        ui.SetActive(Seen);

        zombieAnimator.speed = IsStunned ? 0f : Speed / BaseSpeed; // TODO: Ask David about this PlayerController.Instance.Damageable
        if (!IsStunned)
        {
            Move();
            if (Vector3.Distance(player.position, transform.position) <= 1.5f)
            {
                Attack();
            }
        }
        if (HP <= 0)
        {
            Alive = false;
            StartCoroutine(ZombieDeath());
        }
    }

    private void Move()
    {
        // Move towards the player
        Vector3 directionToPlayer = player.position - transform.position;
        Vector3 newPosition = Vector3.MoveTowards(transform.position, player.position, Speed * Time.deltaTime);
        transform.position = newPosition;

        // Rotate to face the player
        if (directionToPlayer != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, RotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            blood.Play();
            Damage(50, false);
        }

        if (collision.gameObject.CompareTag("Barrel"))
        {
            Damage(50, false);
        }
    }

    public IEnumerator ZombieDeath()
    {
        //Remove Collider
        enemyCollider.enabled = false;

        //Add Money
        StatsManager.Instance.AddMoney(ClassValue);
        StatsManager.Instance.AddMultiplier(ClassMultiplier);

        //Disables HealthBar
        ui.SetActive(false);

        //Mute Audio
        breathing.mute = true;

        //Death Animation
        zombieAnimator.speed = Speed / BaseSpeed;
        zombieAnimator.SetBool("isDead", true);

        //Blood Patch
        Instantiate(guts, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);

        //Let The Body On The Floor
        yield return new WaitForSeconds(6);

        //Destroy Object 
        Destroy(this.gameObject);
    }

    public void Damage(float damage, bool isFromLight)
    {
        if (isFromLight)
        {
            if (HP - damage <= 0)
            {
                return;
            }
        }

        HP -= damage;
    }

    protected virtual void Attack()
    {
    }
}