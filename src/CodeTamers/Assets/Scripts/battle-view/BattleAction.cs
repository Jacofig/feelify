public enum BattleActionType
{
    Attack,
    Block,
    Catch
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
