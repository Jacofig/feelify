using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleActionExecutor : MonoBehaviour
{
    public BattleManager battleManager;
    public float actionDelay = 1f;
    private bool catchUsedThisTurn = false;

    public IEnumerator ExecuteAction(
    Creature owner,
    List<Creature> targets,
    BattleAction action,
    System.Action<bool> onFinished
)
    {
        
        bool didSomething = false;

        if (action.Type == BattleActionType.Catch && catchUsedThisTurn)
        {
            Debug.Log("Catch used more than once this turn — mana wasted");
            owner.TrySpendMana(1); // mana przepada
            onFinished(false);
            yield break;
        }

        if (!owner.TrySpendMana(1))
        {
            onFinished(false);
            yield break;
        }

        switch (action.Type)
        {
            case BattleActionType.Attack:
                didSomething = battleManager.PlayerAttack(
                    owner, targets, action.TargetIndex
                );
                break;

            case BattleActionType.Block:
                battleManager.PlayerBlock(owner, action.TargetIndex);
                didSomething = true;
                break;

            case BattleActionType.Catch:
                didSomething = battleManager.PlayerCatch(
                    owner, targets, action.TargetIndex
                );
                if (didSomething)
                    catchUsedThisTurn = true;
                break;

        }

        if (didSomething)
        {
            battleManager.RefreshAllUI();
            yield return new WaitForSeconds(actionDelay);
        }

        onFinished(didSomething);
    }

    public void ResetTurn()
    {
        catchUsedThisTurn = false;
    }


}
