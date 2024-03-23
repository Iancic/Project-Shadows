using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject lights;
    public AudioSource factorySounds, musicBox;
    public AudioSource outTage;
    public float fuelMax, fuelCurrent = 10.00f;

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
            if (Spawner.Instance.wave == 1)
                fuelMax = Spawner.Instance.waveLenght1;
            else if (Spawner.Instance.wave == 2)
                fuelMax = Spawner.Instance.waveLenght2;
            else if (Spawner.Instance.wave == 3)
                fuelMax = Spawner.Instance.waveLenght3;

            Spawner.Instance.wave += 1;
            outTage.Play();
            goneOut = true;
        }
        else if (fuelCurrent >= fuelMax && goneOut == true)
        {
            goneOut = false;


            fuelMax = 20f; //BUY TIME
            fuelCurrent = 20f; //BUY TIME

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                if (enemy.GetComponent<EnemyController>().Alive == true)
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
