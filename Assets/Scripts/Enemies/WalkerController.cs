using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerController : EnemyController
{

    private void Awake()
    {
        //Walker Stats
        BaseSpeed = 4.5f;

        RotationSpeed = 8f;
        AttackSpeed = 1f;
        AttackDamage = 40f;
        MaxHP = 100f;

        ClassValue = 100;
        ClassMultiplier = 1;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        Debug.Log("Walker Attack");
    }
}
