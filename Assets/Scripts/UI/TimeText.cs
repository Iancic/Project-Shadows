using TMPro;
using UnityEngine;

public class TimeText : MonoBehaviour
{
    public TMP_Text text;

    private int hour, minutes;
    private float minutestext;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        hour = 23;
        minutestext = 0f;
    }

    void Update()
    {
        if (minutestext < 10)
            text.SetText(hour.ToString() + ":" + "0" + Mathf.RoundToInt(minutestext).ToString());
        else if (minutestext > 10)
            text.SetText(hour.ToString() + ":" + Mathf.RoundToInt(minutestext).ToString());

        if (minutestext == 60)
        {
            hour++;
            minutestext = 0f;
        }

        minutestext += Time.deltaTime;
    }
}
