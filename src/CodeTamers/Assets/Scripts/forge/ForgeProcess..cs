using System.Collections.Generic;

public class ForgeProcess
{
    public Metal metal;
    public List<ForgeAction> executedActions = new();

    public bool failed;
    public string failReason;

    public void RegisterAction(ForgeAction action)
    {
        executedActions.Add(action);
    }

    public void Fail(string reason)
    {
        failed = true;
        failReason = reason;
    }
}
