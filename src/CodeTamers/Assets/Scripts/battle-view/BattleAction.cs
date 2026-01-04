public enum BattleActionType
{
    Attack,
    Block
}

public class BattleAction
{
    public BattleActionType Type;
    public int TargetIndex;

    public BattleAction(BattleActionType type, int targetIndex = -1)
    {
        Type = type;
        TargetIndex = targetIndex;
    }

}
