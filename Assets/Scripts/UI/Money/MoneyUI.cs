using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    public TMP_Text text;
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        text.SetText((StatsManager.Instance.Money).ToString());
    }
}
