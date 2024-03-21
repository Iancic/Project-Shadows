using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class AttackerController : PlayerController
{
    protected override void Start()
    {
        base.Start();
        Flashlight.Init(Color.red);
    }

    protected override void Update()
    {
        base.Update();
    
        foreach (var enemy in _enemiesInRange)
        {
            enemy.Damage(1, true);
        }
    }
}