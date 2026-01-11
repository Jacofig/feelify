using UnityEngine;
using System.Collections.Generic;

public class BattleCommandHandler : MonoBehaviour, IGameCommandHandler
{
    private List<BattleAction> actions = new();

    public void ResetActions()
    {
        actions.Clear();
    }

    public List<BattleAction> GetActions()
    {
        return actions;
    }

    public bool CanExecute(string commandName)
    {
        // Interpreter pyta: „czy komenda istnieje”
        return commandName == "attack" || commandName == "block" || commandName == "catch";
    }

    public bool ExecuteCommand(string commandName, string[] args)
    {
        switch (commandName)
        {
            case "attack":
                int target = 0;
                if (args.Length > 0)
                    int.TryParse(args[0], out target);

                actions.Add(new BattleAction(BattleActionType.Attack, target));
                return true;

            case "block":
                int blockTarget = -1;
                if (args.Length > 0)
                    int.TryParse(args[0], out blockTarget);

                actions.Add(new BattleAction(BattleActionType.Block, blockTarget));
                return true;

            case "catch":
                int catchTarget = 0;
                if (args.Length > 0)
                    int.TryParse(args[0], out catchTarget);

                actions.Add(new BattleAction(BattleActionType.Catch, catchTarget));
                return true;


        }

        return false;
    }
}
