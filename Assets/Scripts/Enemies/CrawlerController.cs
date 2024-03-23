using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerController : EnemyController
{
    private void Awake()
    {
        //Crawler Stats
        BaseSpeed = 8f;

        RotationSpeed = 10f;
        AttackSpeed = 5f;
        AttackDamage = 80f;
        MaxHP = 200f;

        ClassValue = 100;
        ClassMultiplier = 1;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        Debug.Log("Crawler Attack");
    }
}
