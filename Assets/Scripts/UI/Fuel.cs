using TMPro;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    public TMP_Text text;

    public GameObject image;

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (PlayerController.Instance.currentFuel != 0)
        {
            image.SetActive(true);
            text.SetText(PlayerController.Instance.currentFuel.ToString());
        }
        else
        {
            image.SetActive(false);
            text.SetText("");
        }
    }
}
