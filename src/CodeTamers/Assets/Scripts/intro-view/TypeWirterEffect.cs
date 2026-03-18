using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    public float typingSpeed = 0.04f;

    private Coroutine typingCoroutine;
    private bool isTyping;

    public void ShowText(string text)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        textUI.text = text;
        textUI.maxVisibleCharacters = 0;

        typingCoroutine = StartCoroutine(TypeText(text));
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;

        for (int i = 0; i <= text.Length; i++)
        {
            textUI.maxVisibleCharacters = i;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    public void Skip()
    {
        if (!isTyping) return;

        StopCoroutine(typingCoroutine);
        textUI.maxVisibleCharacters = textUI.text.Length;
        isTyping = false;
    }

    public bool IsTyping()
    {
        return isTyping;
    }
}