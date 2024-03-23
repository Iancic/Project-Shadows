using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerController : EnemyController
{
    protected override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        Debug.Log("Crawler Attack");
    }
}
