using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;

    public int Money, Multiplier = 1;
    
    private void Awake()
    {
        Money = 0;

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
        Money += amount * Multiplier;
    }

    public void AddMultiplier(int amount)
    {
        Multiplier += amount;
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
