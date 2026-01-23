using TMPro;
using UnityEngine;

public class ObjectiveUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text objectiveText;
    [SerializeField] private RectTransform arrow;

    [Header("Arrow rotation")]
    [SerializeField] private float expandedRotation = 0f;
    [SerializeField] private float collapsedRotation = 180f;

    [SerializeField] private GameObject extraButton; // Twój drugi przycisk


    private bool expanded = true;

    void Start()
    {
        UpdateUIInstant();
    }

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

    public void Toggle()
    {
        expanded = !expanded;

        panel.SetActive(expanded);
        objectiveText.gameObject.SetActive(expanded);

        if (arrow != null)
        {
            arrow.localEulerAngles =
                new Vector3(0f, 0f, expanded ? expandedRotation : collapsedRotation);
        }
        if (extraButton != null)
            extraButton.SetActive(expanded);
    }

    void Refresh()
    {
        var qm = QuestManager.Instance;
        var selected = qm.selectedQuest;

        if (selected == null)
        {
            objectiveText.text = "";
            return;
        }

        var questData = selected.quest;
        var objective = questData.objectives[selected.currentObjectiveIndex];

        objectiveText.text =
            $"🎯 {objective.targetId}\n{selected.currentAmount}/{objective.requiredAmount}";
    }

    private void UpdateUIInstant()
    {
        panel.SetActive(expanded);
        objectiveText.gameObject.SetActive(expanded);

        if (arrow != null)
        {
            arrow.localEulerAngles =
                new Vector3(0f, 0f, expanded ? expandedRotation : collapsedRotation);
        }
        if (extraButton != null)
            extraButton.SetActive(expanded);

    }
}
