using UnityEngine.UI;
using UnityEngine;

public class XpBar : MonoBehaviour
{
    public Image fillImage;

    void Update()
    {
        float fillAmount = (float)PlayerController.Instance.currentXP / PlayerController.Instance.maxXP;
        fillImage.fillAmount = fillAmount;
    }
}
