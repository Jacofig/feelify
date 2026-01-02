using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterText : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    [TextArea(3, 10)]
    public string fullText;
    public float typingSpeed = 0.04f;

    private Coroutine typingCoroutine;
    private bool isTyping;

    void OnEnable()
    {
        StartTyping();
    }

    public void StartTyping()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        isTyping = true;
        textUI.text = "";

        foreach (char letter in fullText)
        {
            textUI.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    public void SkipTyping()
    {
        if (!isTyping) return;

        StopCoroutine(typingCoroutine);
        textUI.text = fullText;
        isTyping = false;
    }

    public bool IsTyping()
    {
        return isTyping;
    }
}
