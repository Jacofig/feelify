using UnityEngine;

public class BattleCommandHandler : MonoBehaviour, IGameCommandHandler
{
    public BattleManager battleManager;

    public void ExecuteCommand(string commandName)
    {
        switch (commandName)
        {
            case "attack":
                battleManager.PlayerAttack();
                break;

            case "block":
                battleManager.PlayerBlock();
                break;
        }
    }
}
