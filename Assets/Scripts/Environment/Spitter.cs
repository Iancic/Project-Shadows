using UnityEngine;

public class Spitter : MonoBehaviour
{

    public GameObject meatBlock;

    public static Spitter Instance { get; private set; }

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
        
    }

    public void SpawnMeat()
    {
        Instantiate(meatBlock, new Vector3(-3.5f, 3f, 26f), Quaternion.identity);
    }
}
