using TMPro;
using UnityEngine;

public class ObjectiveUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text objectiveText;
    
    [SerializeField] private RectTransform arrow;


    [Header("Arrow rotation")]
    [SerializeField] private float expandedRotation = 0f;   // np. strzałka w górę
    [SerializeField] private float collapsedRotation = 180f;


    private bool expanded = true;

    void Start()
    {
        
        UpdateUIInstant();
    }

    public void Toggle()
    {
        expanded = !expanded;
        // ustawienie widoczności od razu
        panel.SetActive(expanded);
        objectiveText.gameObject.SetActive(expanded);

        if (arrow != null)
        {
            arrow.localEulerAngles = new Vector3(0f, 0f, expanded ? expandedRotation : collapsedRotation);
        }

    }

    

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
        var qm = QuestManager.Instance;

        if (qm.currentQuest == null)
        {
            objectiveText.text = "";
            return;
        }

        var obj = qm.CurrentObjective;
        int progress = qm.GetCurrentAmount();

        objectiveText.text = $"🎯 {obj.description}\n{progress}/{obj.requiredAmount}";
    }

    private void UpdateUIInstant()
    {
       

        panel.SetActive(expanded);
        objectiveText.gameObject.SetActive(expanded);
        if (arrow != null)
        {
            arrow.localEulerAngles = new Vector3(0f, 0f, expanded ? expandedRotation : collapsedRotation);
        }
    }
}
