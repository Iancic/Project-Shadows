using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Components
    public Transform player;
    public ParticleSystem blood;
    private Animator zombieAnimator;
    public GameObject ammo;
    public GameObject battery;
    public AudioSource breathing;

    //Movement Speed
    private float speed = 3.8f, rotationSpeed = 7f;

    //Logic
    private int hitPoints = 1;
    public bool alive = true;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        zombieAnimator = gameObject.GetComponent<Animator>();
        breathing = gameObject.GetComponent<AudioSource>();
        breathing.pitch = Random.Range(0.6f, 1f);
        breathing.Play();
    }

    void Update()
    {
        if (alive && PlayerController.Instance.isImmune == false)
        {
            breathing.mute = false;
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
            breathing.mute = true;
            zombieAnimator.speed = 0f;
        }

        if (hitPoints <= 0)
        {
            alive = false;
            StartCoroutine(ZombieDeath());
        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            blood.Play();
            hitPoints = hitPoints - 1;
        }

        if (collision.gameObject.tag == "Light")
        {
            PlayerController.Instance.isImmune = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Light"))
        {
            PlayerController.Instance.isImmune = false;
        }
    }

    public IEnumerator ZombieDeath()
    {
        zombieAnimator.speed = 1f;
        zombieAnimator.SetBool("isDead", true);
        yield return new WaitForSeconds(4);
        Instantiate(battery, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
        Destroy(this.gameObject);
    }
}
