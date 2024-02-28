using TMPro;
using UnityEngine;

public class GeneratorText : MonoBehaviour
{
    public TMP_Text numbers;
    void Start()
    {
        numbers = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (Generator.Instance.goneOut == false)
        {
            numbers.SetText((Mathf.Round(Generator.Instance.fuelCurrent * 100) / 100).ToString());
        }
        else if (Generator.Instance.goneOut == true)
        {
            numbers.SetText("-" + (Generator.Instance.fuelMax - (Mathf.Round(Generator.Instance.fuelCurrent * 100) / 100)).ToString());
        }
    }
}
