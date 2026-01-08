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

        // Ustaw cały tekst od razu, TMP przelicza Auto Size
        textUI.text = fullText;

        // Ustawiamy tylko widoczność liter na 0
        textUI.maxVisibleCharacters = 0;

        typingCoroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        isTyping = true;
        textUI.maxVisibleCharacters = 0;

        for (int i = 0; i < fullText.Length; i++)
        {
            textUI.maxVisibleCharacters = i + 1; // pokazujemy kolejną literę
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    public void SkipTyping()
    {
        if (!isTyping) return;

        StopCoroutine(typingCoroutine);
        textUI.maxVisibleCharacters = fullText.Length;
        isTyping = false;
    }

    public bool IsTyping()
    {
        return isTyping;
    }
}
