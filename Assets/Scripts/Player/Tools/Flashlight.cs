using System;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject SpotLight;
    public AudioSource flashlightClick;
    public DetectionCone Cone;

    public bool isOn = false;
    
    [HideInInspector] public float batteryMax = 60.00f, batteryCurrent = 20.00f;
    public int Range = 10;

    private Light _lightComp;
    private List<EnemyController> _enemiesInCone = new List<EnemyController>();

    public void Init(Color lightColor)
    {
        Range = (int) UpgradesManager.Instance.GetValue(UpgradeType.FlashlightRange);
        Cone = GetComponentInChildren<DetectionCone>();
        _lightComp = SpotLight.GetComponent<Light>();
        Cone.OnEnemyEnter += (e) =>
        {
            _enemiesInCone.Add(e);
            e.Seen = true;
        };
        
        Cone.OnEnemyExit += (e) =>
        {
            // Reset the values to defaults
            e.IsStunned = false;
            e.Speed = e.BaseSpeed;
            _enemiesInCone.Remove(e);
            e.Seen = false;
        };
        
        UpgradesManager.Instance.OnUpgradeChanged += (type, f) =>
        {
            if (type == UpgradeType.FlashlightRange)
            {
                Range = (int) f;
                _lightComp.range = Range;
            }
        };

        _lightComp.color = lightColor;
    }

    public List<EnemyController> Logic()
    {
        SpotLight.SetActive(isOn);

        //Don't let the battery exceed the limit
        batteryCurrent = Mathf.Clamp(batteryCurrent, 0, batteryMax);

        //Battery Life
        if (isOn)
        {
            batteryCurrent -= Time.deltaTime;
        }
        else
        {
            foreach (var enemy in _enemiesInCone)
            {
                // Reset the values to defaults
                enemy.IsStunned = false;
                enemy.Speed = enemy.BaseSpeed;
                enemy.Seen = false;
            }
            _enemiesInCone.Clear();
        }

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
            }
            GunController.Instance.CanShoot = !isOn;
        }

        //If battery goes out
        if (batteryCurrent <= 0)
        {
            isOn = false;
            GunController.Instance.CanShoot = true;
            StatsManager.Instance.Multiplier = 1; //Reset Score Multiplier
        }

        return _enemiesInCone;
    }
}