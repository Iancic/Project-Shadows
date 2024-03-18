using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultiplierUI : MonoBehaviour
{
    public TMP_Text text;
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        text.SetText("x" + (StatsManager.Instance.Multiplier).ToString());
    }
}
