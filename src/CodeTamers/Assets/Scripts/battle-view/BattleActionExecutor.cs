using UnityEngine;
using System.Collections.Generic;


public class BattleActionExecutor : MonoBehaviour
{
    public BattleManager battleManager;

    public bool Execute(
     Creature owner,
     List<Creature> targets,
     BattleAction action
 )
    {
        if (owner.currentMana <= 0)
            return false;

        owner.currentMana--;

        switch (action.Type)
        {
            case BattleActionType.Attack:
                return battleManager.PlayerAttack(owner, targets, action.TargetIndex);

            case BattleActionType.Block:
                battleManager.PlayerBlock(owner, action.TargetIndex);
                return true;
        }

        return false;
    }

}
