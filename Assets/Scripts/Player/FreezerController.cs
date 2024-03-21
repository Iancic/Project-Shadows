using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class FreezerController : PlayerController
{
    protected override void Start()
    {
        base.Start();
        Flashlight.Init(Color.blue);
    }

    protected override void Update()
    {
        base.Update();

        foreach (var enemy in _enemiesInRange)
        {
            // TODO: Freeze logic here
            enemy.Speed = enemy.BaseSpeed / 3;
            Debug.Log("Freezing enemy");
        }
    }
}