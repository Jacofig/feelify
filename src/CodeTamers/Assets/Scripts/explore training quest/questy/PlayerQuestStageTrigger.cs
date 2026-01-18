using UnityEngine;

public class PlayerQuestStageTrigger : MonoBehaviour
{
    [Header("Dialogue Trigger do odpalenia")]
    public DialogueTrigger dialogueTrigger; // przeci¹gasz gracza w Inspectorze
    public int stageIndexToStart;           // który stage odpaliæ

    [Header("Opcjonalnie - tylko po tym queœcie")]
    public QuestData requiredQuest;         // jeœli puste, odpala po ka¿dym zakoñczonym queœcie

    void OnEnable()
    {
        QuestManager.Instance.OnQuestCompletedWithData += OnQuestCompleted;
    }

    void OnDisable()
    {
        if (QuestManager.Instance != null)
            QuestManager.Instance.OnQuestCompletedWithData -= OnQuestCompleted;
    }

    private void OnQuestCompleted(QuestData finishedQuest)
    {
        // jeœli przypisano requiredQuest, odpala tylko po nim
        if (requiredQuest != null && finishedQuest != requiredQuest)
            return;

        // odpala stage w istniej¹cym DialogueTrigger gracza
        dialogueTrigger.StartStageByIndex(stageIndexToStart);
    }
}
