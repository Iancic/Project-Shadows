using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    //Components
    public Transform player;
    public ParticleSystem blood;
    private Animator zombieAnimator;
    public GameObject guts;
    public AudioSource breathing;
    public BoxCollider enemyCollider;

    public Image healthBarImage;
    public GameObject ui;

    //Movement Speed
    public float BaseSpeed = 4.5f;
    public float Speed = 4.5f;
    public float RotationSpeed = 8f;

    //Zombie Values (Class value means the value of the enemy, multiplier is the value of the multipler at death)
    public int classValue = 100, classMultiplier = 1;


    //Logic
    private int hitPoints, maxHitPoints = 3;
    public bool alive = true;
    public bool isStunned = false;

    private void Awake()
    {
        hitPoints = maxHitPoints;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        enemyCollider = gameObject.GetComponent<BoxCollider>();
        zombieAnimator = gameObject.GetComponent<Animator>();

        //Audio Initialization
        breathing = gameObject.GetComponent<AudioSource>();
        breathing.pitch = Random.Range(0.6f, 1f);
        breathing.Play();
    }

    void Update()
    {
        float fillAmount = (float) hitPoints / maxHitPoints;
        healthBarImage.fillAmount = fillAmount;

        if (isStunned && alive == true)
        {
            ui.SetActive(true);
        }
        else
        {
            ui.SetActive(false);
        }

        if (alive)
        {
            if (PlayerController.Instance.Damageable == false && isStunned == false)
            {
                zombieAnimator.speed = Speed / BaseSpeed;
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

            else
            {
                zombieAnimator.speed = 0f;
            }

            if (hitPoints <= 0)
            {
                alive = false;
                StartCoroutine(ZombieDeath());
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            blood.Play();
            // TODO: Modularize damage here
            // TODO: Damage upgrade
            hitPoints = hitPoints - 1;
        }

        if (collision.gameObject.CompareTag("Barrel"))
        {
            hitPoints -= 10; //Barrel.DAMAGE
        }
    }

    public IEnumerator ZombieDeath()
    {
        //Remove Collider
        enemyCollider.enabled = false;

        //Add Money
        StatsManager.Instance.AddMoney(classValue);
        StatsManager.Instance.AddMultiplier(classMultiplier);

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

    public void Damage(int damage, bool isFromLight)
    {
        if (isFromLight)
        {
            if (hitPoints - damage <= 0)
            {
                return;
            }
            
            hitPoints -= (int) damage;
        }
    }
}