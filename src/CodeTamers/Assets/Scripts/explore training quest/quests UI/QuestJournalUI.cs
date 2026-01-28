using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestJournalUI : MonoBehaviour
{
    public GameObject journalPanel;

    public TMP_Text questNameText;
    public TMP_Text questDescriptionText;

    public Button nextButton;
    public Button prevButton;

    private int currentJournalIndex = 0; // lokalny indeks tylko dla dziennika

    public Button showInObjectiveButton;
    public ObjectiveUI objectiveUI;

    void Start()
    {
        if (showInObjectiveButton != null)
            showInObjectiveButton.onClick.AddListener(ShowQuestInObjectiveUI);

        if (nextButton != null)
            nextButton.onClick.AddListener(NextQuest);

        if (prevButton != null)
            prevButton.onClick.AddListener(PrevQuest);
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
        if (currentJournalIndex >= quests.Count) currentJournalIndex = quests.Count - 1;
        if (currentJournalIndex < 0) currentJournalIndex = 0;

        var quest = quests[currentJournalIndex];

        // UWAGA: nie zmieniamy selectedQuest! To jest tylko UI
        questNameText.text = quest.quest.questName;
        questDescriptionText.text = quest.quest.objectives[quest.currentObjectiveIndex].description;

        prevButton.interactable = currentJournalIndex > 0;
        nextButton.interactable = currentJournalIndex < quests.Count - 1;
    }

    public void NextQuest()
    {
        currentJournalIndex++;
        Refresh();
    }

    public void PrevQuest()
    {
        currentJournalIndex--;
        Refresh();
    }

    public void Toggle()
    {
        journalPanel.SetActive(!journalPanel.activeSelf);

        if (journalPanel.activeSelf)
        {
            currentJournalIndex = QuestManager.Instance.activeQuests.Count - 1;
            Refresh();
        }
    }

    void ShowQuestInObjectiveUI()
    {
        var quests = QuestManager.Instance.activeQuests;
        if (quests.Count == 0) return;

        var quest = quests[currentJournalIndex];

        // Przekazujemy wybrany quest tylko do UI celu, nie zmieniamy selectedQuest
        if (objectiveUI != null)
            objectiveUI.SetQuestForDisplay(quest);
    }
}
