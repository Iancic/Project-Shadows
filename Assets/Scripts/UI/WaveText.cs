using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveText : MonoBehaviour
{
    public TMP_Text text;
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        text.SetText("Wave " + (Spawner.Instance.wave).ToString());
    }
}
