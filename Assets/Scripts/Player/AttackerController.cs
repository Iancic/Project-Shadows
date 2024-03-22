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
            // TODO: Tweak this value, might be a tad overpowered now, because with 60 FPS, we are dealing 30 damage per second
            enemy.Damage(0.5f, true);
        }
    }
}