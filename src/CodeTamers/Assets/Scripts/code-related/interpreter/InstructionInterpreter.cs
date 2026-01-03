using UnityEngine;
using System.Collections.Generic;

public class InstructionInterpreter : MonoBehaviour
{
    public Dictionary<string, float> NumberVars = new();
    public Dictionary<string, bool> BoolVars = new();

    [Header("Command Handling")]
    public MonoBehaviour commandHandler;

    public bool Execute(List<ParsedInstruction> instructions)
    {
        foreach (var instr in instructions)
        {
            bool ok = ExecuteInstruction(instr);
            if (!ok)
                return false; // PRZERWIJ PROGRAM
        }
        return true;
    }

    private bool ExecuteInstruction(ParsedInstruction instr)
    {
        switch (instr.Type)
        {
            case InstructionType.GameCommand:
                return ExecuteGameCommand(instr);

            case InstructionType.If:
                return ExecuteIf(instr);

            case InstructionType.While:
                return ExecuteWhile(instr);

            case InstructionType.Assignment:
                ExecuteAssignment(instr);
                return true;
        }
        return true;
    }

    private bool ExecuteGameCommand(ParsedInstruction instr)
    {
        if (commandHandler == null)
            return false;

        var handler = commandHandler as IGameCommandHandler;
        if (handler == null)
            return false;

        if (!handler.CanExecute(instr.Name))
        {
            Debug.Log($"Command blocked: {instr.Name}");
            return false;
        }

        return handler.ExecuteCommand(instr.Name, instr.Arguments);
    }

    private bool ExecuteIf(ParsedInstruction instr)
    {
        if (!EvaluateCondition(instr.Condition))
            return true;

        foreach (var child in instr.Children)
        {
            if (!ExecuteInstruction(child))
                return false;
        }
        return true;
    }

    private bool ExecuteWhile(ParsedInstruction instr)
    {
        int guard = 0;

        while (EvaluateCondition(instr.Condition))
        {
            guard++;
            if (guard > 1000)
            {
                Debug.LogError("WHILE guard break");
                return false;
            }

            foreach (var child in instr.Children)
            {
                if (!ExecuteInstruction(child))
                    return false;
            }
        }
        return true;
    }

    private bool EvaluateCondition(string condition)
    {
        if (BoolVars.TryGetValue(condition, out bool b))
            return b;

        if (bool.TryParse(condition, out bool literal))
            return literal;

        if (condition.Contains(">"))
        {
            var p = condition.Split('>');
            return NumberVars[p[0].Trim()] > float.Parse(p[1].Trim());
        }

        if (condition.Contains("<"))
        {
            var p = condition.Split('<');
            return NumberVars[p[0].Trim()] < float.Parse(p[1].Trim());
        }

        return false;
    }

    private void ExecuteAssignment(ParsedInstruction instr)
    {
        if (float.TryParse(instr.Arguments[0], out float v))
            NumberVars[instr.Name] = v;
    }
}
