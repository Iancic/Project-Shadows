using System.Collections;
using UnityEngine;

public class Bulb : MonoBehaviour
{
    public GameObject SpotLight;

    void Start()
    {
        StartCoroutine(Flicker());
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
