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
    public Image healthBarImage;

    //Movement Speed
    private float speed = 4.5f, rotationSpeed = 8f;

    //Logic
    private int hitPoints, maxHitPoints = 3;
    public bool alive = true;
    public bool isStunned = false;

    private void Awake()
    {
        hitPoints = maxHitPoints;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        zombieAnimator = gameObject.GetComponent<Animator>();
        breathing = gameObject.GetComponent<AudioSource>();
        breathing.pitch = Random.Range(0.6f, 1f);
        breathing.Play();
    }

    void Update()
    {

        float fillAmount = (float)hitPoints / maxHitPoints;
        healthBarImage.fillAmount = fillAmount;

        if (alive)
        {
            if (PlayerController.Instance.isImmune == false && isStunned == false)
            {
                zombieAnimator.speed = 1f;
                // Move towards the player
                Vector3 directionToPlayer = player.position - transform.position;
                Vector3 newPosition = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                transform.position = newPosition;

                // Rotate to face the player
                if (directionToPlayer != Vector3.zero)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
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
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Light")
        {
            isStunned = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Light"))
        {
            isStunned = false;
        }
    }

    public IEnumerator ZombieDeath()
    {
        breathing.mute = true;
        zombieAnimator.speed = 1f;
        zombieAnimator.SetBool("isDead", true);
        Instantiate(guts, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(6);
        Destroy(this.gameObject);
    }
}
