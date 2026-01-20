public class ForgeAction
{
    public ForgeActionType Type;
    public string Argument;

    public ForgeAction(ForgeActionType type, string arg = null)
    {
        Type = type;
        Argument = arg;
    }
}
