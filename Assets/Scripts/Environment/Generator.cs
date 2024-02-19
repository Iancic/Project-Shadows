using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject lights;
    public AudioSource factorySounds;
    public AudioSource outTage;
    public float fuelMax = 60.00f, fuelCurrent = 60.00f;

    public bool canSpawn;
    private bool goneOut = true;

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

        if (fuelCurrent <= 0f && goneOut == false)
        {
            outTage.Play();
            goneOut = true;
        }

        if (fuelCurrent > 0f)
        {
            goneOut = false;
            canSpawn = false;
            lights.SetActive(true);
            fuelCurrent -= Time.deltaTime;
        }
        else
        {
            canSpawn = true;
            lights.SetActive(false);
            factorySounds.Pause();
        }

    }
}
