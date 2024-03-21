using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class StunnerController : PlayerController
{
    protected override void Start()
    {
        base.Start();
        Flashlight.Init(Color.yellow);
    }

    protected override void Update()
    {
        base.Update();
    
        Debug.Log(_enemiesInRange.Count);
        foreach (var enemy in _enemiesInRange)
        {
            enemy.isStunned = true;
        }
    }
}