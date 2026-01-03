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

        baseInterpreter.NumberVars["hp"] = owner.currentHP;
        baseInterpreter.NumberVars["mana"] = owner.currentMana;

        bool ok = baseInterpreter.Execute(instructions);

        owner.currentHP = Mathf.RoundToInt(baseInterpreter.NumberVars["hp"]);

        if (!ok)
            return null;

        return battleHandler.GetActions();
    }
}
