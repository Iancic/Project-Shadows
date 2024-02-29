using TMPro;
using UnityEngine;

public class TimeText : MonoBehaviour
{
    public TMP_Text text;

    private float minutesLeft = 15, secondsLeft = 20;

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        secondsLeft -= Time.deltaTime;

        if (secondsLeft <= 0)
        {
            secondsLeft = 60;
            minutesLeft -= 1;
        }
        if (secondsLeft > 9)
            text.SetText(" Time Left: " + minutesLeft.ToString() + ":" + Mathf.RoundToInt(secondsLeft).ToString());
        else
            text.SetText(" Time Left: " + minutesLeft.ToString() + ":0" + Mathf.RoundToInt(secondsLeft).ToString());
    }
}
