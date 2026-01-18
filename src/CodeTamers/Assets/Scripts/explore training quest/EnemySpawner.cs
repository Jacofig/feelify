using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public QuestData requiredQuest;       // quest, po którym spawner ma siê uruchomiæ
    public GameObject[] enemiesToSpawn;

    private bool spawned = false;

    void OnEnable()
    {
        QuestManager.Instance.OnQuestStarted += OnQuestStarted;
    }

    void OnDisable()
    {
        if (QuestManager.Instance != null)
            QuestManager.Instance.OnQuestStarted -= OnQuestStarted;
    }

    void OnQuestStarted(QuestData quest)
    {
        if (spawned) return;              // nie spawnuj ponownie
        if (quest != requiredQuest) return; // tylko jeœli quest siê zgadza

        foreach (var enemy in enemiesToSpawn)
        {
            enemy.SetActive(true);
        }

        spawned = true;
    }
}
