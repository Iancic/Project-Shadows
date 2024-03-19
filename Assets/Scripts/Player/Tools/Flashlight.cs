using System;
using UnityEngine;
public class Flashlight : MonoBehaviour
{

    public bool isOn = false;

    public GameObject SpotLight;
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

        if (PlayerController.Instance.batteryCurrent < 0)
        {
            isOn = false;
            StatsManager.Instance.Multiplier = 1;

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
                enemy.GetComponent<EnemyController>().isStunned = false;

            ShootBullet.Instance.isSelected = true;
        }    
    }
}
