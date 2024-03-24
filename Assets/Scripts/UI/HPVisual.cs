using UnityEngine;
using UnityEngine.UI;

public class HPVisual : MonoBehaviour
{
    public Image fillImage;

    void Update()
    {
        float fillAmount = PlayerController.Instance.HP / PlayerController.Instance.MaxHP;
        fillImage.fillAmount = fillAmount;
    }
}
