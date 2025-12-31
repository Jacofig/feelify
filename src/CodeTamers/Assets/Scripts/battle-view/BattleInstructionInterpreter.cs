using System.Collections.Generic;
using UnityEngine;

public class BattleInstructionInterpreter : MonoBehaviour
{
    public InstructionInterpreter baseInterpreter;

    public void Execute(Creature owner, List<ParsedInstruction> instructions)
    {
        // Map creature state → interpreter variables
        baseInterpreter.NumberVars["hp"] = owner.currentHP;
        baseInterpreter.NumberVars["mana"] = owner.currentMana;

        // You can also map booleans like:
        // baseInterpreter.BoolVars["enemyNear"] = true;

        baseInterpreter.Execute(instructions);

        // After execution, pull values back
        owner.currentHP = Mathf.RoundToInt(baseInterpreter.NumberVars["hp"]);
        owner.currentMana = Mathf.RoundToInt(baseInterpreter.NumberVars["mana"]);
    }
}
