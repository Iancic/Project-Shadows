using System.Collections;
using UnityEngine;

public class Bulb : MonoBehaviour
{
    public GameObject SpotLight;
    public GameObject bulbMetal;

    public float rotSpeed;

    public bool isOn;

    void Start()
    {
        rotSpeed = Random.Range(10f, 20f);
        StartCoroutine(Flicker());
    }

    public void Update()
    {
        bulbMetal.transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0));
    }

    public IEnumerator Flicker()
    {
        while (true){
        isOn = true;
        SpotLight.SetActive(isOn);
        yield return new WaitForSeconds(Random.Range(8f, 10f)); //For how much the bulb is on
        isOn = false;
        SpotLight.SetActive(isOn);
        yield return new WaitForSeconds(Random.Range(3f, 8f)); //For how much the bulb is off
        }
    }
}
