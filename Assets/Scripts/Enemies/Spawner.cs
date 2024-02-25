using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Components
    public GameObject walkerPrefab, crawlerPrefab;
    public Transform playerTransform;

    //Spawn Radius
    private float spawnRadius = 35f, safeZoneRadius = 25f;
    private int maxZombies = 10;
    private float spawnInterval = 3.5f;

    private float timer;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        timer = spawnInterval;
    }

    void Update()
    {
        if (Generator.Instance.canSpawn == true)
        {
            if (timer <= 0 && GameObject.FindGameObjectsWithTag("Zombie").Length < maxZombies)
            {
                SpawnZombie();
                timer = spawnInterval;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

    }

    public void SpawnZombie()
    {
        Vector3 spawnPosition = playerTransform.position + Random.insideUnitSphere * spawnRadius;
        Vector3 spawnPosition2 = playerTransform.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = 0;
        spawnPosition2.y = 0;

        //Check for safe zone
        if ((spawnPosition - playerTransform.position).magnitude < safeZoneRadius)
        {
            return;
        }

        Instantiate(walkerPrefab, spawnPosition, Quaternion.identity);
        Instantiate(crawlerPrefab, spawnPosition2, Quaternion.identity);
    }
}
