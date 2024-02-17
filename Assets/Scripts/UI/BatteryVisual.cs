using UnityEngine;
using UnityEngine.UI;

public class BatteryVisual : MonoBehaviour
{
    public Image fillImage;

    void Update()
    {
        float fillAmount = PlayerController.Instance.batteryCurrent / PlayerController.Instance.batteryMax;
        fillImage.fillAmount = fillAmount;
    }
}
