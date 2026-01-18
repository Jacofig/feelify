using UnityEngine;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{
    [System.Serializable]
    public class DialogueStage
    {
        public Dialogue dialogue;           // może być null
        public MonoBehaviour[] actions;     // komponenty implementujące IDialogueAction
        [HideInInspector] public bool hasRun;
    }

    public DialogueStage[] stages;
    public int currentStage = 0;

    private bool playerInRange;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    void Update()
    {
        if (!playerInRange) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            DialogueManager.Instance.NextLine();
        }
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
        if (currentStage >= stages.Length) return;

        DialogueStage stage = stages[currentStage];

        if (stage.hasRun) return;
        stage.hasRun = true;

        // 🔹 STAGE Z DIALOGIEM
        if (stage.dialogue != null)
        {
            DialogueManager.Instance.StartDialogue(stage.dialogue);
            DialogueManager.Instance.OnDialogueEnd += OnDialogueFinished;
        }
        // 🔹 STAGE TYLKO Z AKCJAMI
        else
        {
            StartCoroutine(RunActions(stage));
        }
    }

    private void OnDialogueFinished()
    {
        DialogueManager.Instance.OnDialogueEnd -= OnDialogueFinished;

        DialogueStage stage = stages[currentStage];
        StartCoroutine(RunActions(stage));
    }

    private IEnumerator RunActions(DialogueStage stage)
    {
        foreach (var action in stage.actions)
        {
            if (action is IDialogueAction dialogueAction)
            {
                bool done = false;
                dialogueAction.Execute(() => done = true);
                yield return new WaitUntil(() => done);
            }
        }

        // 🔑 DOPIERO PO AKCJACH IDZIEMY DALEJ
        currentStage++;
        RunCurrentStage();
    }
}
