using UnityEngine;

public class BattleCommandHandler : MonoBehaviour, IGameCommandHandler
{
    private BattleManager battleManager;
    private Creature owner;

    public void SetContext(BattleManager manager, Creature creature)
    {
        battleManager = manager;
        owner = creature;
    }

    public bool CanExecute(string commandName)
    {
        return owner.currentMana > 0;
    }

    public bool ExecuteCommand(string commandName, string[] args)
    {
        if (owner.currentMana <= 0)
            return false;

        owner.currentMana--; 

        switch (commandName)
        {
            case "attack":
                int target = 0;
                if (args.Length > 0)
                    int.TryParse(args[0], out target);

                return battleManager.PlayerAttack(owner, target);

            case "block":
                battleManager.PlayerBlock(owner);
                return true;
        }

        return false;
    }
}
