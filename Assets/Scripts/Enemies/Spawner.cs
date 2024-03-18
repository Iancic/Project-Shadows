using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Components
    public GameObject walkerPrefab, crawlerPrefab;
    public Transform playerTransform;

    //Spawn Radius
    private float spawnRadius = 45f;

    //Wave Specs
    private float spawnInterval1 = 6f, spawnInterval2 = 4f, spawnInterval3 = 3f; //Wave 1, 2, 3 Spawnrate
    public float waveLenght1 = 30f, waveLenght2 = 40f, waveLenght3 = 60f; //Wave 1, 2, 3 Lenght
    public int wave = 0;

    private float timer;

    public static Spawner Instance { get; private set; }

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

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        timer = spawnInterval1;
    }

    void Update()
    {
        if (Generator.Instance.canSpawn == true)
        {
            if (wave <= 3)
            {
                if (timer <= 0 && GameObject.FindGameObjectsWithTag("Zombie").Length < 5)
                {
                    Spawn(walkerPrefab);
                    timer = spawnInterval1;
                }
                else
                {
                    timer -= Time.deltaTime;
                }
            }

            else if (wave <= 5)
            {
                if (timer <= 0 && GameObject.FindGameObjectsWithTag("Zombie").Length < 8)
                {
                    Spawn(walkerPrefab);
                    Spawn(crawlerPrefab);
                    timer = spawnInterval2;
                }
                else
                {
                    timer -= Time.deltaTime;
                }
            }

            else if (wave <= 10)
            {
                if (timer <= 0 && GameObject.FindGameObjectsWithTag("Zombie").Length < 12)
                {
                    Spawn(walkerPrefab);
                    Spawn(crawlerPrefab);
                    timer = spawnInterval3;
                }
                else
                {
                    timer -= Time.deltaTime;
                }
            }

        }

    }

    public void Spawn(GameObject prefab)
    {
        Vector3 spawnPosition = playerTransform.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = 0;

        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}
