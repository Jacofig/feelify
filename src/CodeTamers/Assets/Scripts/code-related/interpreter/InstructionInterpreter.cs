using UnityEngine;
using System.Collections.Generic;
using System;

public class InstructionInterpreter : MonoBehaviour
{
    public Dictionary<string, float> NumberVars = new();
    public Dictionary<string, bool> BoolVars = new();

    
    public Dictionary<string, Func<float>> NumberFunctions = new();

    [Header("Command Handling")]
    public MonoBehaviour commandHandler;

    public bool Execute(List<ParsedInstruction> instructions)
    {
        foreach (var instr in instructions)
        {
            if (!ExecuteInstruction(instr))
                return false;
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
            return false;

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
        // bool literal
        if (bool.TryParse(condition, out bool literal))
            return literal;

        // porównania
        if (condition.Contains("<"))
        {
            var p = condition.Split('<');
            float left = EvaluateNumber(p[0].Trim());
            float right = float.Parse(p[1].Trim());
            return left < right;
        }

        if (condition.Contains(">"))
        {
            var p = condition.Split('>');
            float left = EvaluateNumber(p[0].Trim());
            float right = float.Parse(p[1].Trim());
            return left > right;
        }

        return false;
    }

    private float EvaluateNumber(string token)
    {
        token = token.Trim();

        
        if (float.TryParse(token, out float literal))
            return literal;

        // zmienna
        if (NumberVars.TryGetValue(token, out float v))
            return v;

        // funkcja bez argumentów
        if (token.EndsWith("()"))
        {
            string fn = token.Replace("()", "");
            if (NumberFunctions.TryGetValue(fn, out var func))
                return func();
        }

        throw new Exception($"Unknown number expression: {token}");
    }


    private float EvaluateAssignmentValue(string expr)
    {
        expr = expr.Trim();

        // obs³uga: a + b
        if (expr.Contains("+"))
        {
            var parts = expr.Split('+');
            if (parts.Length != 2)
                throw new Exception($"Invalid addition expression: {expr}");

            float left = EvaluateNumber(parts[0].Trim());
            float right = EvaluateNumber(parts[1].Trim());
            return left + right;
        }

        // fallback: pojedyncza liczba / zmienna / funkcja
        return EvaluateNumber(expr);
    }

    private void ExecuteAssignment(ParsedInstruction instr)
    {
        float value = EvaluateAssignmentValue(instr.Arguments[0]);
        NumberVars[instr.Name] = value;
    }

}
