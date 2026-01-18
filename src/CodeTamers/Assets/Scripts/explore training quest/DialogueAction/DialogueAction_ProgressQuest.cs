using UnityEngine;

public class DialogueAction_ProgressQuest : MonoBehaviour, IDialogueAction
{
    public string targetId;
    public int amount = 1;

    public void Execute(System.Action onFinished)
    {
        QuestManager.Instance.Progress(targetId, amount);
        onFinished?.Invoke();
    }
}
