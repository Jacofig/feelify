using System.Diagnostics;
using UnityEngine;

public class PlayerQuestStageTrigger : MonoBehaviour
{
    [Header("Player Dialogue Stage Runner do odpalenia")]
    public PlayerDialogueTrigger playerDialogueRunner; // przeciągasz gracza w Inspectorze
    public int stageIndexToStart;           // który stage odpalić
    public string stageIdToStart;           // alternatywnie, jeśli chcesz odpalać po ID

    [Header("Opcjonalnie - tylko po tym queście")]
    public QuestData requiredQuest;         // jeśli puste, odpala po każdym zakończonym queście

    void OnEnable()
    {
        if (QuestManager.Instance != null)
            QuestManager.Instance.OnQuestCompletedWithData += OnQuestCompleted;
    }

    void OnDisable()
    {
        if (QuestManager.Instance != null)
            QuestManager.Instance.OnQuestCompletedWithData -= OnQuestCompleted;
    }

    private void OnQuestCompleted(QuestData finishedQuest)
    {
        // jeśli przypisano requiredQuest, odpala tylko po nim
        if (requiredQuest != null && finishedQuest != requiredQuest)
            return;

        if (playerDialogueRunner == null)
        {
            UnityEngine.Debug.LogWarning("PlayerQuestStageTrigger: playerDialogueRunner nie przypisany!");
            return;
        }

        // 🔹 odpala stage po indeksie (jeśli stageIndexToStart >= 0)
        if (stageIndexToStart >= 0 && stageIndexToStart < playerDialogueRunner.stages.Length)
        {
            playerDialogueRunner.RunStageByIndex(stageIndexToStart);
            UnityEngine.Debug.LogWarning("GRRRRR");
        }
        // 🔹 lub odpala stage po ID (jeśli podano)
        else if (!string.IsNullOrEmpty(stageIdToStart))
        {
            playerDialogueRunner.RunStageById(stageIdToStart);


            UnityEngine.Debug.LogWarning("GRRRRdsadsadR");
        }
        else
        {
            UnityEngine.Debug.LogWarning("PlayerQuestStageTrigger: nie podano stageIndex ani stageId!");
        }
    }
}
