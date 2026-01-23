using UnityEngine;

public class DialogueAction_ProgressQuest : MonoBehaviour, IDialogueAction
{
    public string targetId;
    public int amount = 1;

    public void Execute(System.Action onFinished)
    {
        UnityEngine.Debug.Log($"Executing ProgressQuest: {targetId}, amount: {amount}");
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.Progress(targetId, amount);
            UnityEngine.Debug.Log("Progress done.");
        }
        else
        {
            UnityEngine.Debug.LogError("QuestManager.Instance is null!");
        }

        onFinished?.Invoke();
    }
}
