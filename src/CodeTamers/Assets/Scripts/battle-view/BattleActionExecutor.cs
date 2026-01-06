using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleActionExecutor : MonoBehaviour
{
    public BattleManager battleManager;
    public float actionDelay = 1f;

    public IEnumerator ExecuteAction(
    Creature owner,
    List<Creature> targets,
    BattleAction action,
    System.Action<bool> onFinished
)
    {
        bool didSomething = false;

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
        }

        if (didSomething)
            yield return new WaitForSeconds(actionDelay);

        onFinished(didSomething);
    }

}
