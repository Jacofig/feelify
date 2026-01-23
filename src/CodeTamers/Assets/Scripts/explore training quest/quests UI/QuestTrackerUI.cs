using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class QuestTrackerUI : MonoBehaviour
{
    public TMP_Text questNameText;
    public TMP_Text questObjectiveText;

    void OnEnable()
    {
        QuestManager.Instance.OnObjectiveUpdated += Refresh;
        Refresh();
    }

    void OnDisable()
    {
        QuestManager.Instance.OnObjectiveUpdated -= Refresh;
    }

    void Refresh()
    {
        var quest = QuestManager.Instance.selectedQuest;
        if (quest == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        questNameText.text = quest.quest.questName;
        questObjectiveText.text =
            quest.quest.objectives[quest.currentObjectiveIndex].description;
    }
}
