using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionCone : MonoBehaviour
{
    public Action<EnemyController> OnEnemyEnter;
    public Action<EnemyController> OnEnemyExit;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent<EnemyController>()) return;
        OnEnemyEnter?.Invoke(other.GetComponent<EnemyController>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.GetComponent<EnemyController>()) return;
        OnEnemyExit?.Invoke(other.GetComponent<EnemyController>());
    }
}