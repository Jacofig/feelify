using UnityEngine;
using System;
using System.Diagnostics;
using System.Collections.Generic;


public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public QuestData currentQuest;
    public int currentObjectiveIndex;

    public event Action OnObjectiveUpdated;
    public event Action OnQuestCompleted;
    public event Action<QuestData> OnQuestStarted;

    public event Action<QuestData> OnQuestCompletedWithData;

    private int currentAmount;



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

    public ObjectiveData CurrentObjective =>
        currentQuest.objectives[currentObjectiveIndex];

    public void StartQuest(QuestData quest)
    {
        currentQuest = quest;
        currentObjectiveIndex = 0;
        currentAmount = 0;

        OnQuestStarted?.Invoke(quest);
        OnObjectiveUpdated?.Invoke();
    }

    public void Progress(string targetId, int amount = 1)
    {
        var obj = CurrentObjective;

        if (obj.targetId != targetId)
            return;

        currentAmount += amount;

        if (currentAmount >= obj.requiredAmount)
            CompleteObjective();
        else
            OnObjectiveUpdated?.Invoke();
    }

    void CompleteObjective()
    {
        currentObjectiveIndex++;
        currentAmount = 0;

        if (currentObjectiveIndex >= currentQuest.objectives.Length)
        {
            UnityEngine.Debug.Log("QUEST COMPLETED");


            if (currentQuest != null && !completedQuests.Contains(currentQuest))
                completedQuests.Add(currentQuest);

            QuestData finishedQuest = currentQuest;

            currentQuest = null;



            completedQuestsCount++;

            //OnQuestCompleted?.Invoke();

            OnQuestCompletedWithData?.Invoke(finishedQuest);





            OnObjectiveUpdated?.Invoke();

            CheckConditionalQuests();

        }
        else
        {
            OnObjectiveUpdated?.Invoke();
        }
    }

    public int GetCurrentAmount() => currentAmount;



    void CheckConditionalQuests()
    {
        foreach (var cq in conditionalQuests)
        {
            if (cq.triggered) continue; // ju¿ odpali³o siê wczeœniej

            bool countConditionOk = cq.minCompletedQuests < 0 || completedQuestsCount >= cq.minCompletedQuests;
            bool questConditionOk = cq.requiredQuest == null || completedQuests.Contains(cq.requiredQuest);

            if (countConditionOk && questConditionOk)
            {
                StartQuest(cq.questToStart);
                cq.triggered = true;
                UnityEngine.Debug.Log($"Conditional quest started: {cq.questToStart.questName}");
            }
        }
    }


}
