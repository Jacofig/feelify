using UnityEngine;

public class DialogueAction_StartQuest : MonoBehaviour, IDialogueAction
{
    public QuestData questToStart;

    public void Execute(System.Action onComplete)
    {
        QuestManager.Instance.StartQuest(questToStart);
        onComplete?.Invoke();
    }
}
