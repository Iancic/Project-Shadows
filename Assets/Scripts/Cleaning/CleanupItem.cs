using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanupItem : MonoBehaviour
{
    public int BaseDirtiness = 3;
    public int Reward;

    private int dirtiness;
    private void Start()
    {
        dirtiness = BaseDirtiness;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mop"))
        {
            Mop mop = other.GetComponentInParent<Mop>();
            if (mop != null)
            {
                dirtiness -= mop.Efficiency;
                OnDirtinessChanged();
                if (dirtiness <= 0)
                {
                    Destroy(gameObject);
                    // TODO: Reward clean
                    // GameManager.Instance.AddScore(Reward);
                }
            }
        }
    }

    private void OnDirtinessChanged()
    {
        // fade alpha
        GetComponent<Renderer>().material.color = new Color(1, 1, 1, dirtiness / (float)BaseDirtiness);
    }
}
