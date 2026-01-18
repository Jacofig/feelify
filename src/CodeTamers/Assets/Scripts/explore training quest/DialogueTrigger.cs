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
        public UnityEngine.Events.UnityEvent onStageFinished;
    }

    public DialogueStage[] stages;
    public int currentStage = 0;

    private bool playerInRange;

    private bool stagePaused = false;  // czy dialog jest wstrzymany

    private bool dialogueActive = false;



    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    void Update()
    {
        if (!dialogueActive) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            DialogueManager.Instance.NextLine();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;

        if (stagePaused)
        {
            // wznawiamy dialog zamiast restartować
            DialogueManager.Instance.dialoguePanel.SetActive(true);
            stagePaused = false;
        }
        else
        {
            RunCurrentStage();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;

        if (currentStage < stages.Length && stages[currentStage].dialogue != null)
        {
            // wstrzymujemy dialog i chowamy panel
            DialogueManager.Instance.dialoguePanel.SetActive(false);
            stagePaused = true;
        }
    }


    private void RunCurrentStage()
    {
        if (currentStage >= stages.Length) return;

        DialogueStage stage = stages[currentStage];

        if (stage.hasRun) return;
        stage.hasRun = true;

        dialogueActive = true;
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
        dialogueActive = false;
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


        stage.onStageFinished?.Invoke();


        // 🔑 DOPIERO PO AKCJACH IDZIEMY DALEJ
        currentStage++;
        RunCurrentStage();


        
        

    }

    public void StartStageByIndex(int stageIndex)
    {
        if (stageIndex < 0 || stageIndex >= stages.Length) return;

        currentStage = stageIndex;

        // resetujemy hasRun, żeby stage mógł się wykonać wielokrotnie
        stages[currentStage].hasRun = false;

        RunCurrentStage();
    }

}
