using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject lights;
    public AudioSource factorySounds, musicBox;
    public AudioSource outTage;
    public float fuelMax = 60.00f, fuelCurrent = 10.00f;

    public bool canSpawn;
    public bool goneOut = false;

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
            Debug.Log("asdasd");
            goneOut = true;
        }
        else if (fuelCurrent >= fuelMax && goneOut == true)
        {
            goneOut = false;

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                if (enemy.GetComponent<EnemyController>().alive == true)
                    Destroy(enemy);
            }
        }

        if (goneOut == false)
        {
            canSpawn = false;
            lights.SetActive(true);
            fuelCurrent -= Time.deltaTime; 
            factorySounds.UnPause();
            musicBox.UnPause();
        }
        else if (goneOut == true)
        {
            canSpawn = true;
            lights.SetActive(false);
            fuelCurrent += Time.deltaTime;
            factorySounds.Pause();
            musicBox.Pause();
        }

    }
}
