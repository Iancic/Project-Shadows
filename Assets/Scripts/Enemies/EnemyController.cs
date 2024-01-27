using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Components
    public Transform player;
    public ParticleSystem blood;
    private Animator zombieAnimator;
    public GameObject drop;

    //Movement Speed
    public float speed = 5f, rotationSpeed = 5f;

    //Logic
    private int hitPoints = 1;
    public bool alive = true;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        zombieAnimator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (alive)
        {
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
    }

    public IEnumerator ZombieDeath()
    {
        zombieAnimator.SetBool("isDead", true);
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
        Instantiate(drop, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
    }
}
