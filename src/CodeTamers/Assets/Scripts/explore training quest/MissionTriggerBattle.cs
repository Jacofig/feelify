using UnityEngine;

public class MissionTriggerBattle : MonoBehaviour
{
    [Header("Dialogi")]
    public Dialogue firstDialogue;   // dialog przed walk¹
    public Dialogue secondDialogue;  // dialog po walce (teraz dla testu uruchamia siê po pierwszym)

    private bool playerInRange = false;
    private bool firstDone = false;

    public RiverMovment npc;

    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                if (DialogueManager.Instance != null)
                {
                    DialogueManager.Instance.NextLine();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !firstDone)
        {
            playerInRange = true;

            if (firstDialogue != null)
            {
                DialogueManager.Instance.StartDialogue(firstDialogue);
                DialogueManager.Instance.OnDialogueEnd += StartSecondDialogue;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            DialogueManager.Instance.dialoguePanel.SetActive(false);
        }
    }

    private void StartSecondDialogue()
    {
        DialogueManager.Instance.OnDialogueEnd -= StartSecondDialogue;
        firstDone = true;

        if (secondDialogue != null)
        {
            DialogueManager.Instance.StartDialogue(secondDialogue);
        }
        DialogueManager.Instance.OnDialogueEnd += MoveNPC;
    }

    private void MoveNPC()
    {
        if (npc != null)
        {
            npc.MoveToTarget();
        }
        DialogueManager.Instance.OnDialogueEnd -= MoveNPC; // od³¹cz event, ¿eby nie wywo³ywa³o siê wielokrotnie
    }
}
