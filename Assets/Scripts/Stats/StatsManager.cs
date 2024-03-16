using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;

    public int Money;
    
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
    
    public void AddMoney(int amount)
    {
        Money += amount;
    }
    
    public void RemoveMoney(int amount)
    {
        Money -= amount;
    }
    
    public int GetMoney()
    {
        return Money;
    }
}
