using TMPro;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public TMP_Text text;
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        text.SetText((PlayerController.Instance.HP).ToString());
    }
}
