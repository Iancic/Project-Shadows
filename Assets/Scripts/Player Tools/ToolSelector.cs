using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ToolSelector : MonoBehaviour
{

    public AudioSource flashlightClick;

    private void Awake()
    {
        flashlightClick = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (ShootBullet.Instance.isSelected == false)
            {
                flashlightClick.Play();
                ShootBullet.Instance.isSelected = true;
                Flashlight.Instance.isOn = false;
            }
            else
            {
                flashlightClick.Play();
                ShootBullet.Instance.isSelected = false;
                Flashlight.Instance.isOn = true;
            }
        }
    }
}
