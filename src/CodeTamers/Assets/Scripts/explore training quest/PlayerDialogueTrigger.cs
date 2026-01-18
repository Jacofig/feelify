using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerDialogueTrigger : MonoBehaviour
{
    [System.Serializable]
    public class DialogueStage
    {
        public string stageId;
        public Dialogue dialogue;
        public MonoBehaviour[] actions;
        public UnityEngine.Events.UnityEvent onStageFinished;
    }

    public DialogueStage[] stages;

    private Queue<DialogueStage> queue = new Queue<DialogueStage>();
    private bool stageRunning;

    void Update()
    {
        if (!stageRunning) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            DialogueManager.Instance?.NextLine();
        }
    }

    // 🔹 WYWOŁUJESZ → DODAJE DO KOLEJKI
    public void RunStageByIndex(int index)
    {
        if (index < 0 || index >= stages.Length) return;

        queue.Enqueue(stages[index]);

        if (!stageRunning)
            StartCoroutine(ProcessQueue());
    }

    public void RunStageById(string id)
    {
        foreach (var stage in stages)
        {
            if (stage.stageId == id)
            {
                queue.Enqueue(stage);

                if (!stageRunning)
                    StartCoroutine(ProcessQueue());
                return;
            }
        }
    }

    // 🔹 WYKONUJE TYLKO TO, CO JEST W KOLEJCE
    private IEnumerator ProcessQueue()
    {
        stageRunning = true;

        while (queue.Count > 0)
        {
            yield return RunStage(queue.Dequeue());
        }

        stageRunning = false;
    }

    private IEnumerator RunStage(DialogueStage stage)
    {
        if (stage.dialogue != null)
        {
            DialogueManager.Instance.StartDialogue(stage.dialogue);

            bool dialogueEnded = false;
            System.Action endHandler = () => dialogueEnded = true;

            DialogueManager.Instance.OnDialogueEnd += endHandler;
            yield return new WaitUntil(() => dialogueEnded);
            DialogueManager.Instance.OnDialogueEnd -= endHandler;
        }

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
    }
}
