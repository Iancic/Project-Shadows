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
        text.SetText((Mathf.Round(Generator.Instance.fuelCurrent * 100) / 100).ToString() + " of light");
    }
}
