using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Shop Shop;
    public Image ReloadIcon;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleReloadIcon(bool visible)
    {
        ReloadIcon.gameObject.SetActive(visible);
    }
    
    private void Update()
    {
        if (ReloadIcon.gameObject.activeInHierarchy)
        {
            // rotate on z axis
            ReloadIcon.transform.Rotate(0, 0, 1);
        }
    }

    public void DisplayShop(bool visible)
    {
        Shop.gameObject.SetActive(visible);
    }
}
