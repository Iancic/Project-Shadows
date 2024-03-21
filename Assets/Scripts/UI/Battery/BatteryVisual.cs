using UnityEngine;
using UnityEngine.UI;

public class BatteryVisual : MonoBehaviour
{
    public Image fillImage;

    void Update()
    {
        float fillAmount = PlayerController.Instance.Flashlight.batteryCurrent / PlayerController.Instance.Flashlight.batteryMax;
        fillImage.fillAmount = fillAmount;
    }
}
