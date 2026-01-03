using System.Collections.Generic;
using UnityEngine;

public class RandomAttackStrategy : IEnemyTurnStrategy
{
    public List<BattleAction> GetActions(Creature enemy, List<Creature> playerTeam)
    {
        List<BattleAction> actions = new();

        // enemy atakuje tyle razy, ile ma many
        for (int i = 0; i < enemy.currentMana; i++)
        {
            var aliveTargets = playerTeam.FindAll(c => c.currentHP > 0);
            if (aliveTargets.Count == 0)
                break;

            int targetIndex = playerTeam.IndexOf(
                aliveTargets[Random.Range(0, aliveTargets.Count)]
            );

            actions.Add(new BattleAction(BattleActionType.Attack, targetIndex));
        }

        return actions;
    }
}
