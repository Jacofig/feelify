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
        if (enemy.CurrentHP <= 0)
            return actions;

        int enemyIndex = playerTeam.Count > 0
            ? playerTeam.IndexOf(playerTeam.Find(c => c.CurrentHP > 0))
            : -1;

       
        int selfIndex = enemy.teamIndex; // UWAGA: patrz ni�ej

        if (selfIndex < 0 || selfIndex >= playerTeam.Count)
            return actions;

        if (playerTeam[selfIndex].CurrentHP <= 0)
            return actions;

        actions.Add(new BattleAction(
            BattleActionType.Attack,
            selfIndex
        ));

        return actions;
    }
}
