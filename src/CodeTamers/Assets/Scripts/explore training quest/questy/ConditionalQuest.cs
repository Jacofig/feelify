using UnityEngine;

[System.Serializable]
public class ConditionalQuest
{
    [Header("Quest to start")]
    public QuestData questToStart;      // misja, która ma siê odpaliæ

    [Header("Conditions (optional)")]
    public int minCompletedQuests = -1; // ile ukoñczonych questów wymaga, -1 = ignoruj
    public QuestData requiredQuest;     // quest, który musi byæ ukoñczony, null = ignoruj

    [HideInInspector]
    public bool triggered;              // czy ju¿ siê odpali³a
}
