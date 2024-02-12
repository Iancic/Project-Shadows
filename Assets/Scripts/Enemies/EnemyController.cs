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

    //Movement Speed
    private float speed = 6.5f, rotationSpeed = 6.5f;

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
        if (alive && PlayerController.Instance.isImmune == false)
        {
            zombieAnimator.SetBool("isRunning", true);
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
            zombieAnimator.SetBool("isRunning", false);

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
        int n = Random.Range(0, 1);
        if (n == 0)
            Instantiate(ammo, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
        else if (n == 1)
            Instantiate(battery, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);

        zombieAnimator.SetBool("isDead", true);
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }
}
