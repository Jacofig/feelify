using UnityEngine;

public class DialogueAction_StartQuest : MonoBehaviour, IDialogueAction
{
    public QuestData questToStart;

    public void Execute(System.Action onFinished)
    {
        if (QuestManager.Instance.currentQuest != questToStart)
        {
            QuestManager.Instance.StartQuest(questToStart);
        }

        onFinished?.Invoke();
    }
}
