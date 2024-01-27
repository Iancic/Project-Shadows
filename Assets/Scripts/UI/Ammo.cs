using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public TMP_Text text;
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        text.SetText(ShootBullet.Instance.currentAmmo.ToString() + "/" + ShootBullet.Instance.maxAmmo.ToString());
    }
}
