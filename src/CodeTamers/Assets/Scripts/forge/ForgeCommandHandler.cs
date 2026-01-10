using UnityEngine;
using System.Collections.Generic;

public class ForgeCommandHandler : MonoBehaviour, IGameCommandHandler
{
    private List<ForgeAction> actions = new();

    public void ResetActions()
    {
        actions.Clear();
    }

    public List<ForgeAction> GetActions()
    {
        return actions;
    }

    public bool CanExecute(string commandName)
    {
        return commandName == "hit"
            || commandName == "heat"
            || commandName == "add"
            || commandName == "cast";
    }

    public bool ExecuteCommand(string commandName, string[] args)
    {
        switch (commandName)
        {
            case "hit":
                actions.Add(new ForgeAction(ForgeActionType.Hit));
                return true;

            case "heat":
                actions.Add(new ForgeAction(ForgeActionType.Heat));
                return true;

            case "add":
                if (args.Length == 0) return false;
                actions.Add(new ForgeAction(ForgeActionType.Add, args[0]));
                return true;

            case "cast":
                actions.Add(new ForgeAction(ForgeActionType.Cast));
                return true;
        }

        return false;
    }
}
