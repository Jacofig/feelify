using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class QuestJournalUI : MonoBehaviour
{

    public GameObject journalPanel;


    public TMP_Text questNameText;
    public TMP_Text questDescriptionText;

    public Button nextButton;
    public Button prevButton;
    private int currentIndex = 0;

    public Button showInObjectiveButton;
    public ObjectiveUI objectiveUI; // referencja do ObjectiveUI

    void Start()
    {
        if (showInObjectiveButton != null)
            showInObjectiveButton.onClick.AddListener(ShowQuestInObjectiveUI);
    }


    void OnEnable()
    {
        Refresh();
    }

    void Refresh()
    {
        var quests = QuestManager.Instance.activeQuests;

        if (quests.Count == 0)
        {
            questNameText.text = "Brak aktywnych questów";
            questDescriptionText.text = "";
            nextButton.interactable = false;
            prevButton.interactable = false;
            return;
        }

        // ograniczenie indeksu
        if (currentIndex >= quests.Count) currentIndex = quests.Count - 1;
        if (currentIndex < 0) currentIndex = 0;

        var quest = quests[currentIndex];
        QuestManager.Instance.selectedQuest = quest;

        questNameText.text = quest.quest.questName;
        questDescriptionText.text = quest.quest.objectives[quest.currentObjectiveIndex].description;

        // ustawienie przycisków
        prevButton.interactable = currentIndex > 0;
        nextButton.interactable = currentIndex < quests.Count - 1;
    }

    public void NextQuest()
    {
        currentIndex++;
        Refresh();
    }

    public void PrevQuest()
    {
        currentIndex--;
        Refresh();
    }

    public void Toggle()
    {
        journalPanel.SetActive(!journalPanel.activeSelf);
        if (journalPanel.activeSelf)
        {
            //currentIndex = 0; // pokazujemy pierwszy quest
            currentIndex = QuestManager.Instance.activeQuests.Count - 1;
            Refresh();
        }
    }
    void ShowQuestInObjectiveUI()
    {
        var quests = QuestManager.Instance.activeQuests;
        if (quests.Count == 0) return;

        var quest = quests[currentIndex];
        QuestManager.Instance.selectedQuest = quest;

        if (objectiveUI != null)
            objectiveUI.Refresh(); // odświeżamy UI celu
    }


}
