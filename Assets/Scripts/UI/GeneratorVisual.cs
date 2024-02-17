using UnityEngine.UI;
using UnityEngine;

public class GeneratorVisual : MonoBehaviour
{
    public Image fillImage;

    void Update()
    {
        float fillAmount = Generator.Instance.fuelCurrent / Generator.Instance.fuelMax;
        fillImage.fillAmount = fillAmount;
    }
}
