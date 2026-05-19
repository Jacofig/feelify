using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ForgeDialogueManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    private Dialogue currentDialogue;
    private int lineIndex;

    public ScrollRect scrollRect;
    public TypewriterEffect typewriter;

    void Awake()
    {
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

        var line = currentDialogue.lines[lineIndex];

        typewriter.ShowText(line.text ?? "");
    }

    public void NextLine()
    {
        lineIndex++;

        if (lineIndex >= currentDialogue.lines.Length)
        {
            dialoguePanel.SetActive(false);
            return;
        }

        ShowLine();
    }
}