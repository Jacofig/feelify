public enum BattleActionType
{
    Attack,
    Block
}

public class BattleAction
{
    public BattleActionType Type;
    public int TargetIndex;

    public BattleAction(BattleActionType type, int targetIndex = 0)
    {
        Type = type;
        TargetIndex = targetIndex;
    }
}
