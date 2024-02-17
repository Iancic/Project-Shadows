using TMPro;
using UnityEngine;

public class BatteryText : MonoBehaviour
{
    public TMP_Text text;
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        text.SetText((Mathf.Round(PlayerController.Instance.batteryCurrent * 100) / 100).ToString()  + " sec");
    }
}
