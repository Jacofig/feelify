using UnityEngine;
using System;
using System.Diagnostics;
using System.Collections.Generic;


public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [System.Serializable]
    public class ActiveQuest
    {
        public QuestData quest;
        public int currentObjectiveIndex;
        public int currentAmount;
    }


    public event Action OnObjectiveUpdated;
    public event Action OnQuestCompleted;
    public event Action<QuestData> OnQuestStarted;

    public event Action<QuestData> OnQuestCompletedWithData;

    private int currentAmount;


    public List<ActiveQuest> activeQuests = new List<ActiveQuest>();
    public ActiveQuest selectedQuest;




    public int completedQuestsCount = 0; // globalny licznik ukoñczonych misji
    public ConditionalQuest[] conditionalQuests; // lista warunkowych misji

    public List<QuestData> completedQuests = new List<QuestData>();


    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public ObjectiveData CurrentObjective
    {
        get
        {
            if (selectedQuest == null)
                return null;

            return selectedQuest.quest
                .objectives[selectedQuest.currentObjectiveIndex];
        }
    }


    public void StartQuest(QuestData quest)
    {
        if (activeQuests.Exists(q => q.quest == quest))
            return;

        ActiveQuest newQuest = new ActiveQuest
        {
            quest = quest,
            currentObjectiveIndex = 0,
            currentAmount = 0
        };

        activeQuests.Add(newQuest);
        selectedQuest = newQuest;

        OnQuestStarted?.Invoke(quest);
        OnObjectiveUpdated?.Invoke();
    }
    public void Progress(string targetId, int amount = 1)
    {
        for (int i = activeQuests.Count - 1; i >= 0; i--)
        {
            var quest = activeQuests[i];
            var obj = quest.quest.objectives[quest.currentObjectiveIndex];

            if (obj.targetId != targetId)
                continue;

            quest.currentAmount += amount;

            if (quest.currentAmount >= obj.requiredAmount)
                CompleteObjective(quest);
            else
                OnObjectiveUpdated?.Invoke();
        }

    }


    void CompleteObjective(ActiveQuest quest)
    {
        quest.currentObjectiveIndex++;
        quest.currentAmount = 0;

        if (quest.currentObjectiveIndex >= quest.quest.objectives.Length)
        {
            activeQuests.Remove(quest);
            completedQuests.Add(quest.quest);
            completedQuestsCount++;

            OnQuestCompletedWithData?.Invoke(quest.quest);
            CheckConditionalQuests();
        }

        OnObjectiveUpdated?.Invoke();
    }


    public int GetCurrentAmount() => currentAmount;



    void CheckConditionalQuests()
    {
        foreach (var cq in conditionalQuests)
        {
            if (cq.triggered) continue; // ju¿ odpali³o siê wczeœniej

            bool countConditionOk = cq.minCompletedQuests < 0 || completedQuestsCount >= cq.minCompletedQuests;
            bool questConditionOk =
     cq.requiredQuest == null ||
     IsQuestCompleted(cq.requiredQuest.questName);


            if (countConditionOk && questConditionOk)
            {
                StartQuest(cq.questToStart);
                cq.triggered = true;
                UnityEngine.Debug.Log($"Conditional quest started: {cq.questToStart.questName}");
            }
        }
    }

    public bool IsQuestCompleted(string questName)
    {
        foreach (var q in completedQuests)
        {
            if (q.questName == questName)
                return true;
        }

        return false;
    }

}
