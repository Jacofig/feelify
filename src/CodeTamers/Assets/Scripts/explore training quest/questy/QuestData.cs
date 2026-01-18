using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Quest")]
public class QuestData : ScriptableObject
{
    public string questName;
    [TextArea] public string description;

    public ObjectiveData[] objectives;
}





/*

QuestManager.Instance.Progress("Forest");


if (playerWon)
{
    QuestManager.Instance.Progress("Enemy_Trainer");
}

QuestManager.Instance.Progress("Loop", 1);
*/