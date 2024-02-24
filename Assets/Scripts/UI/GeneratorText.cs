using TMPro;
using UnityEngine;

public class GeneratorText : MonoBehaviour
{
    public TMP_Text text;
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (Generator.Instance.goneOut == false)
            text.SetText((Mathf.Round(Generator.Instance.fuelCurrent * 100) / 100).ToString() + " of light");
        else if (Generator.Instance.goneOut == true)
            text.SetText((Generator.Instance.fuelMax - (Mathf.Round(Generator.Instance.fuelCurrent * 100) / 100)).ToString() + " until recharge");
    }
}
