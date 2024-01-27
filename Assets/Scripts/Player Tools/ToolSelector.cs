using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ToolSelector : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (ShootBullet.Instance.isSelected == false)
            {
                ShootBullet.Instance.isSelected = true;
                Flashlight.Instance.isOn = false;
            }
            else
            {
                ShootBullet.Instance.isSelected = false;
                Flashlight.Instance.isOn = true;
            }
        }
    }
}
