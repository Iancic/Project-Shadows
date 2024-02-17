using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject lights;
    public float fuelMax = 30.00f, fuelCurrent = 30.00f;

    public bool canSpawn;

    public static Generator Instance { get; private set; }

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

    void Update()
    {
        if (fuelCurrent > 0f)
        {
            canSpawn = false;
            lights.SetActive(true);
            fuelCurrent -= Time.deltaTime;
        }
        else
        {
            canSpawn = true;
            lights.SetActive(false);
        }
    }
}
