using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public TMP_Text speakerNameText;
    public UnityEngine.UI.Image speakerIconImage;

    private Dialogue currentDialogue;
    private int lineIndex;

    public event System.Action OnDialogueEnd;
    public ScrollRect scrollRect;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        dialoguePanel.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        lineIndex = 0;
        dialoguePanel.SetActive(true);
        ShowLine();
    }
    void ShowLine()
    {
        if (currentDialogue == null) return;
        if (currentDialogue.lines == null || currentDialogue.lines.Length == 0) return;

        if (lineIndex < 0 || lineIndex >= currentDialogue.lines.Length)
        {
            EndDialogue();
            return;
        }

        var line = currentDialogue.lines[lineIndex];

        if (line == null) return;
        if (dialogueText == null) return;

        dialogueText.text = line.text ?? "";
        speakerNameText.text = line.speakerName ?? "";
        speakerIconImage.sprite = line.speakerIcon;

        ResetScroll(); // <<< TO JEST KLUCZ
    }

    void ResetScroll()
    {
        if (!scrollRect) return; // ← jeśli nie ma, nic nie rób

        StartCoroutine(ResetScrollCoroutine());
    }

    IEnumerator ResetScrollCoroutine()
    {
        yield return null;

        if (!scrollRect) yield break;

        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 1f;
    }



    public void NextLine()
    {
        lineIndex++;
        ShowLine();
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        currentDialogue = null;
        OnDialogueEnd?.Invoke();
    }
}
