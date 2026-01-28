using TMPro;
using UnityEngine;

public class ObjectiveUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text objectiveText;

    private QuestManager.ActiveQuest displayedQuest;

    void OnEnable()
    {
        QuestManager.Instance.OnObjectiveUpdated += Refresh;
        Refresh();
    }

    void OnDisable()
    {
        if (QuestManager.Instance != null)
            QuestManager.Instance.OnObjectiveUpdated -= Refresh;
    }

    public void SetQuestForDisplay(QuestManager.ActiveQuest quest)
    {
        displayedQuest = quest;
        Refresh();
    }

    public void Refresh()
    {
        var qm = QuestManager.Instance;
        QuestManager.ActiveQuest questToShow = null;

        // 1️⃣ Jeśli wybrano quest z dziennika i nadal jest aktywny
        if (displayedQuest != null && qm.activeQuests.Contains(displayedQuest))
        {
            questToShow = displayedQuest;
        }
        else
        {
            // 2️⃣ Szukamy pierwszego aktywnego questa
            if (qm.activeQuests.Count > 0)
            {
                questToShow = qm.activeQuests[qm.activeQuests.Count - 1];
            }
        }

        // 3️⃣ Jeśli nie ma żadnego aktywnego questa
        if (questToShow == null)
        {
            objectiveText.text = " Brak aktywnych misji";
            return;
        }

        // 4️⃣ Szukamy pierwszego nieukończonego celu w tym quest
        int objectiveIndex = Mathf.Min(questToShow.currentObjectiveIndex, questToShow.quest.objectives.Length - 1);
        var objective = questToShow.quest.objectives[objectiveIndex];
        int currentAmount = (objectiveIndex == questToShow.currentObjectiveIndex) ? questToShow.currentAmount : 0;

        objectiveText.text = $" {questToShow.quest.questName}\n{currentAmount}/{objective.requiredAmount}";
    }
}
