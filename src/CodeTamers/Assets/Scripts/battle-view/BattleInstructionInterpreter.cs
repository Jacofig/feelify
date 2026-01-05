using UnityEngine;
using System.Collections.Generic;

public class BattleInstructionInterpreter : MonoBehaviour
{
    public InstructionInterpreter baseInterpreter;
    public BattleCommandHandler battleHandler;

    public List<BattleAction> Execute(Creature owner, List<ParsedInstruction> instructions)
    {
        battleHandler.ResetActions();

        baseInterpreter.commandHandler = battleHandler;

        // Read-only inputs for code decisions
        baseInterpreter.NumberVars["hp"] = owner.CurrentHP;
        baseInterpreter.NumberVars["mana"] = owner.CurrentMana;

        bool ok = baseInterpreter.Execute(instructions);

        if (!ok)
            return null;

        return battleHandler.GetActions();
    }
}
