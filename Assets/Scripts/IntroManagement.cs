using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManagement : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject Generator;
    public GameObject Gun;
    public GameObject HP;
    public GameObject Wave;
    public GameObject Currency;
    public GameObject Battery;
    public GameObject Card;
    public GameObject CardInfo;
    public GameObject Logs;

    public GameObject Lights;

    public bool GameStarted = false;

    public bool Showing = false;

    public bool CardAquired = false;
    public bool LogsAquired = false;

    public static IntroManagement Instance { get; private set; }

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

        //UI
        Generator.SetActive(false);
        Gun.SetActive(false);
        HP.SetActive(false);
        Wave.SetActive(false);
        Currency.SetActive(false);  
        Battery.SetActive(false);
        Card.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && CardAquired == true && GameStarted == false)
        {
            if (Showing == false)
            {
                Showing = true;
                CardInfo.SetActive(true);
            }

            else
            {
                Showing = false;
                CardInfo.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && LogsAquired)
        {
            StartGame();
        }
    }

    public void PickGun()
    {
        Gun.SetActive(true);
    }

    public void PickCard()
    {
        Card.SetActive(true);
        CardAquired = true;
    }

    public void ShowLogs()
    {
        Logs.SetActive(true);
        LogsAquired = true;
    }

    public void StartGame()
    {
        GameStarted = true;

        //UI Activation
        Generator.SetActive(true);
        HP.SetActive(true);
        Wave.SetActive(true);
        Currency.SetActive(true);
        Battery.SetActive(true);

        //Card & Logs UI Deactivation
        Card.SetActive(false);
        Logs.SetActive(false);

        //Lights
        Lights.SetActive(false);
    }
}
