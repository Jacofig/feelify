using UnityEngine;
using TMPro;

public class PlayerSpeechBubble : MonoBehaviour
{
    public TextMeshProUGUI textField;

    public void ShowBubble(string msg)
    {
        if (textField == null)
        {
            Debug.LogError("Brak przypisanego TextMeshProUGUI w PlayerSpeechBubble!");
            return;
        }

        textField.text = msg;
        gameObject.SetActive(true);
    }

    public void HideBubble()
    {
        gameObject.SetActive(false);
    }
}
