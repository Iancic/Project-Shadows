using UnityEngine;
using UnityEngine.UI;

public class BatteryVisual : MonoBehaviour
{
    public Image fillImage;

    void Update()
    {
        float fillAmount = Flashlight.Instance.batteryCurrent / Flashlight.Instance.batteryMax;
        fillImage.fillAmount = fillAmount;
    }
}
