using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [System.Serializable]
    public class DialogueStage
    {
        public Dialogue dialogue;           // może być null
        public MonoBehaviour[] actions;     // akcje po dialogu
        public bool hasRun = false;         // czy Stage został już wykonany
    }

    public DialogueStage[] stages;
    public int currentStage = 0;
    private bool playerInRange = false;

    void Update()
    {
        if (!playerInRange) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            DialogueManager.Instance.NextLine();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;
        RunCurrentStage();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }

    private void RunCurrentStage()
    {
        // dopóki są Stage'y i Stage nie został wykonany
        while (currentStage < stages.Length)
        {
            var stage = stages[currentStage];

            if (stage.hasRun) break;

            stage.hasRun = true;

            if (stage.dialogue != null)
            {
                // Stage ma dialog → startujemy dialog i wychodzimy z pętli
                DialogueManager.Instance.StartDialogue(stage.dialogue);
                DialogueManager.Instance.OnDialogueEnd += OnStageFinished;
                break; // czekamy na zakończenie dialogu
            }
            else
            {
                // Stage bez dialogu → od razu wykonujemy akcje i przechodzimy do następnego Stage
                ExecuteStageActions(stage);
                currentStage++;
            }
        }
    }

    private void OnStageFinished()
    {
        DialogueManager.Instance.OnDialogueEnd -= OnStageFinished;

        var stage = stages[currentStage];
        ExecuteStageActions(stage);

        currentStage++;
        RunCurrentStage(); // od razu uruchamiamy kolejny Stage
    }

    private void ExecuteStageActions(DialogueStage stage)
    {
        foreach (var action in stage.actions)
        {
            if (action is IDialogueAction dialogueAction)
                dialogueAction.Execute();
        }
    }
}
