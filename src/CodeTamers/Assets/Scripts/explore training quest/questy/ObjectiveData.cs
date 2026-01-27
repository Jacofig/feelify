using UnityEngine;

public enum ObjectiveType
{
    GoToLocation,
    DefeatEnemy,
    CatchBugon,
    CraftRecipe
}

[System.Serializable]
public class ObjectiveData
{
    public ObjectiveType type;
    public string description;

    public string targetId;   // np. "Forest", "Enemy_001", "Berry"
    public int requiredAmount;
}
