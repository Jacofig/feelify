using System.Collections.Generic;

public class EnemyMirrorAttackStrategy : IEnemyTurnStrategy
{
    public List<BattleAction> GetActions(
        Creature enemy,
        List<Creature> playerTeam
    )
    {
        List<BattleAction> actions = new();

        // atakuje TYLKO RAZ
        if (enemy.currentHP <= 0)
            return actions;

        int enemyIndex = playerTeam.Count > 0
            ? playerTeam.IndexOf(playerTeam.Find(c => c.currentHP > 0))
            : -1;

       
        int selfIndex = enemy.teamIndex; // UWAGA: patrz ni¿ej

        if (selfIndex < 0 || selfIndex >= playerTeam.Count)
            return actions;

        if (playerTeam[selfIndex].currentHP <= 0)
            return actions;

        actions.Add(new BattleAction(
            BattleActionType.Attack,
            selfIndex
        ));

        return actions;
    }
}
