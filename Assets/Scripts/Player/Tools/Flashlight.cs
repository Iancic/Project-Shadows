using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class Flashlight : MonoBehaviour
{

    public bool isOn = true;

    public GameObject SpotLight;

    public static Flashlight Instance { get; private set; }

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

    private void Start()
    {
        isOn = false;
    }

    void Update()
    {
        if (isOn)
        {
            SpotLight.SetActive(true);
        }
        else
        {
            SpotLight.SetActive(false);
        }
    }
}
