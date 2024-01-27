using System.Collections;
using UnityEngine;

public class Bulb : MonoBehaviour
{
    public GameObject SpotLight;
    public GameObject bulbMetal;

    public float rotSpeed;

    void Start()
    {
        rotSpeed = Random.Range(10f, 20f);
        StartCoroutine(Flicker());
    }

    private void Update()
    {
        bulbMetal.transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0));
    }

    public IEnumerator Flicker()
    {
        while (true) {
        SpotLight.SetActive(true);
        yield return new WaitForSeconds(10f); //For how much the bulb is on
        SpotLight.SetActive(false);
        yield return new WaitForSeconds(Random.Range(3f, 8f)); //For how much the bulb is off
        }
    }
}
