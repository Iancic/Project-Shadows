using UnityEngine;

public class PickUps : MonoBehaviour
{
    private Vector3 playerPos;
    private Vector3 enemyPos;
    private Vector3 currentPos;

    private float distance;

    public GameObject text;

    public GameObject player;

    public AudioSource pickupSound;

    public Outline outlineScript;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        currentPos = transform.position;
        playerPos = player.transform.position;
        
        distance = Vector3.Distance(currentPos, playerPos);

        //Visual Text
        if (this.gameObject.CompareTag("Transformer"))
        {
            if (distance < 5f)
            {
                outlineScript.enabled = true;
                text.SetActive(true);
            }
            else
            {
                outlineScript.enabled = false;
                text.SetActive(false);
            }
        }
        else
        {
            if (distance < 3.5f)
            {
                outlineScript.enabled = true;
                text.SetActive(true);
            }
            else
            {
                outlineScript.enabled = false;
                text.SetActive(false);
            }
        }

        //Interaction
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (this.gameObject.CompareTag("Ammo") && distance < 3.5f)
            {
                pickupSound.Play();
                player.GetComponent<PlayerController>().currentAmmo += 1;
                Destroy(this.gameObject, 0.2f);
            }

            else if (this.gameObject.CompareTag("Battery") && distance < 3.5f)
            {
                pickupSound.Play();
                player.GetComponent<PlayerController>().batteryCurrent += 10f;
                Destroy(this.gameObject, 0.2f);
            }

            else if (this.gameObject.CompareTag("Organs") && distance < 5f)
            {
                player.GetComponent<PlayerController>().isCarrying = true;
                Destroy(this.gameObject);
            }

            else if (this.gameObject.CompareTag("Transformer") && distance < 5f && player.GetComponent<PlayerController>().isCarrying)
            {
                player.GetComponent<PlayerController>().isCarrying = false;
                Spitter.Instance.SpawnMeat();
                pickupSound.Play();
            }

        }
    }
    
}
