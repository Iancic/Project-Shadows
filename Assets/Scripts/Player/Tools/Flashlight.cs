using System;
using UnityEngine;
public class Flashlight : MonoBehaviour
{
    public GameObject SpotLight;
    public AudioSource flashlightClick;

    public bool isOn = false;

    [HideInInspector] public float batteryMax = 60.00f, batteryCurrent = 20.00f;

    public int Range = 10;

    public static Flashlight Instance { get; private set; }

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

    }

    private void Start()
    {
        Range = (int) UpgradesManager.Instance.GetValue(UpgradeType.FlashlightRange);
        UpgradesManager.Instance.OnUpgradeChanged += (type, f) =>
        {
            if (type == UpgradeType.FlashlightRange)
            {
                Range = (int) f;
                SpotLight.GetComponent<Light>().range = Range;
            }
        };
    }

    void Update()
    {
        SpotLight.SetActive(isOn);

        //Don't let the battery exceed the limit
        if (batteryCurrent > batteryMax)
            batteryCurrent = batteryMax;

        //Battery Life
        if (Flashlight.Instance.isOn == true)
            batteryCurrent -= Time.deltaTime;

        //Turn On At Click
        if (Input.GetMouseButtonDown(1) && batteryCurrent > 0)
        {
            if (isOn == false)
            {
                flashlightClick.Play();
                isOn = true;
            }
            else if (isOn == true)
            {
                flashlightClick.Play();
                isOn = false;
                StatsManager.Instance.Multiplier = 1; //Reset Score Multiplier

                //Un stun all enemies when you close the light
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in enemies)
                    enemy.GetComponent<EnemyController>().isStunned = false;
            }
        }

        //If battery goes out
        if (batteryCurrent <= 0)
        {
            isOn = false;
            StatsManager.Instance.Multiplier = 1; //Reset Score Multiplier

            //Un stun all enemies when you close the light
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
                enemy.GetComponent<EnemyController>().isStunned = false;
        }    
    }
}
