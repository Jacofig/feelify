using System.Collections.Generic;

public interface IEnemyTurnStrategy
{
    List<BattleAction> GetActions(Creature enemy, List<Creature> playerTeam);
}
