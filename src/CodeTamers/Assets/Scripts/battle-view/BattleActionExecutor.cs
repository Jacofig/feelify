using UnityEngine;

public class BattleActionExecutor : MonoBehaviour
{
    public BattleManager battleManager;

    public bool Execute(Creature owner, BattleAction action)
    {
        // koszt many = 1 (jak by³o)
        if (owner.currentMana <= 0)
        {
            Debug.Log("No mana – stop program");
            return false;
        }

        owner.currentMana--;

        switch (action.Type)
        {
            case BattleActionType.Attack:
                return battleManager.PlayerAttack(owner, action.TargetIndex);

            case BattleActionType.Block:
                battleManager.PlayerBlock(owner);
                return true;
        }

        return false;
    }
}
