using UnityEngine;
using UnityEngine.SceneManagement;

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

    void Update()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        currentPos = transform.position;
        playerPos = player.transform.position;
        
        distance = Vector3.Distance(currentPos, playerPos);

        //Visual Text
        if (this.gameObject.CompareTag("Computer"))
        {
            if (distance < 6.5f)
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
            if (this.gameObject.CompareTag("BatteryDispenser") && distance < 3.5f && StatsManager.Instance.Money - 100 >= 0)
            {
                pickupSound.Play();
                PlayerController.Instance.Flashlight.batteryCurrent += 15f;
                StatsManager.Instance.RemoveMoney(100);
            }

            if (this.gameObject.CompareTag("HealthDispenser") && distance < 3.5f && StatsManager.Instance.Money - 100 >= 0)
            {
                pickupSound.Play();
                player.GetComponent<PlayerController>().HP += 15;
                StatsManager.Instance.RemoveMoney(100);
            }

            if (this.gameObject.CompareTag("Door") && distance < 3.5f)
                SceneManager.LoadScene("MainRoom");

            if (gameObject.CompareTag("Computer") && distance < 6.5f)
            {
                if (IntroManagement.Instance.GameStarted == false)
                {
                    if (IntroManagement.Instance.CardAquired == true)
                    {
                        IntroManagement.Instance.ShowLogs();
                        
                    }
                }
                else if (IntroManagement.Instance.GameStarted == true)
                {
                    UIManager.Instance.DisplayShop(true);
                }
            }

            if (gameObject.CompareTag("Shotgun") && distance < 3.5f)
            {
                pickupSound.Play();
                IntroManagement.Instance.PickGun();
                Destroy(this.gameObject);
            }


            if (gameObject.CompareTag("Card") && distance < 3.5f)
            {
                pickupSound.Play();
                IntroManagement.Instance.PickCard();
                Destroy(this.gameObject);
            }

        }
    }
    
}
