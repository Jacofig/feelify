using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class InstructionInterpreter : MonoBehaviour
{
    public PlayerSpeechBubble playerBubble;
    // Zmienne liczbowe (np. hp, mana, distance itp.)
    public Dictionary<string, float> NumberVars = new Dictionary<string, float>();

    // Zmienne boolowskie (np. enemyNear, isBoss itp.)
    public Dictionary<string, bool> BoolVars = new Dictionary<string, bool>();

    private float displayTime = 1.2f; // ile czasu dymek jest widoczny dla ka¿dej komendy

    void Start()
    {
        NumberVars["hp"] = 70f;
        NumberVars["distance"] = 5f;

        BoolVars["enemyNear"] = true;
        BoolVars["isBoss"] = false;
    }

    public void Execute(List<ParsedInstruction> instructions)
    {
        StopAllCoroutines();
        StartCoroutine(RunInstructions(instructions));
    }

    private IEnumerator RunInstructions(List<ParsedInstruction> instructions)
    {
        foreach (var instr in instructions)
        {
            yield return ExecuteInstruction(instr);
        }
    }

    private IEnumerator ExecuteInstruction(ParsedInstruction instr)
    {
        switch (instr.Type)
        {
            case InstructionType.GameCommand:
                yield return ExecuteGameCommand(instr);
                break;

            case InstructionType.If:
                yield return ExecuteIf(instr);
                break;

            case InstructionType.While:
                yield return ExecuteWhile(instr);
                break;
            case InstructionType.Assignment:
                ExecuteAssignment(instr);
                break;
        }
    }

    private IEnumerator ExecuteGameCommand(ParsedInstruction instr)
    {
        if (instr.Name == "attack")
        {
            playerBubble.ShowBubble("Atakujê!");
        }
        else if (instr.Name == "block")
        {
            playerBubble.ShowBubble("Blokujê!");
        }

        yield return new WaitForSeconds(displayTime);
        playerBubble.HideBubble();
    }

    //SPRAWDZANIE WARUNKU
    private bool EvaluateCondition(string condition)
    {
        if (string.IsNullOrWhiteSpace(condition))
            return false;

        condition = condition.Trim();

        // 1. Prosty przypadek: pojedyncze True / False albo nazwa bool zmiennej
        if (BoolVars.ContainsKey(condition))
            return BoolVars[condition];

        if (bool.TryParse(condition, out bool singleBool))
            return singleBool;

        // 2. Porównania: ==, !=, >, <, >=, <=
        string[] operators = { "==", "!=", ">=", "<=", ">", "<" };

        foreach (string op in operators)
        {
            var parts = condition.Split(new[] { op }, StringSplitOptions.None);
            if (parts.Length == 2)
            {
                string leftToken = parts[0].Trim();
                string rightToken = parts[1].Trim();

                // Spróbuj traktowaæ jako bool (np. isBoss == True, enemyNear == False)
                if (LooksLikeBool(leftToken) || LooksLikeBool(rightToken))
                {
                    bool leftBool = GetBoolValue(leftToken);
                    bool rightBool = GetBoolValue(rightToken);

                    switch (op)
                    {
                        case "==": return leftBool == rightBool;
                        case "!=": return leftBool != rightBool;
                        default:
                            Debug.LogError("Operator " + op + " nie jest obs³ugiwany dla booli");
                            return false;
                    }
                }

                // W przeciwnym razie traktujemy jako liczby (np. hp > 50)
                float leftNum = GetNumberValue(leftToken);
                float rightNum = GetNumberValue(rightToken);

                switch (op)
                {
                    case "==": return Mathf.Approximately(leftNum, rightNum);
                    case "!=": return !Mathf.Approximately(leftNum, rightNum);
                    case ">": return leftNum > rightNum;
                    case "<": return leftNum < rightNum;
                    case ">=": return leftNum >= rightNum;
                    case "<=": return leftNum <= rightNum;
                }
            }
        }

        Debug.LogError("Nieznany format warunku: " + condition);
        return false;
    }

    // Czy token wygl¹da jak bool albo nazwa bool zmiennej
    private bool LooksLikeBool(string token)
    {
        token = token.Trim();
        if (BoolVars.ContainsKey(token)) return true;
        if (bool.TryParse(token, out _)) return true;
        return false;
    }

    // Pobranie wartoœci bool: literal (True/False) albo zmienna
    private bool GetBoolValue(string token)
    {
        token = token.Trim();

        if (BoolVars.TryGetValue(token, out bool b))
            return b;

        if (bool.TryParse(token, out bool literal))
            return literal;

        Debug.LogError("Nieznana zmienna bool: " + token);
        return false;
    }

    // Pobranie wartoœci liczbowej: literal (10, 3.5) albo zmienna
    private float GetNumberValue(string token)
    {
        token = token.Trim();

        if (NumberVars.TryGetValue(token, out float num))
            return num;

        if (float.TryParse(token, System.Globalization.NumberStyles.Float,
                           System.Globalization.CultureInfo.InvariantCulture,
                           out float literal))
            return literal;

        Debug.LogError("Nieznana zmienna liczbowa: " + token);
        return 0f;
    }


    private IEnumerator ExecuteIf(ParsedInstruction instr)
    {
        if (EvaluateCondition(instr.Condition))
        {
            foreach (var child in instr.Children)
            {
                yield return ExecuteInstruction(child);
            }
        }
    }

    private IEnumerator ExecuteWhile(ParsedInstruction instr)
    {
        // Bezpiecznik — ¿eby nie powiesiæ gry
        int maxIterations = 1000;
        int counter = 0;

        while (EvaluateCondition(instr.Condition))
        {
            foreach (var child in instr.Children)
            {
                yield return ExecuteInstruction(child);
            }

            counter++;
            if (counter > maxIterations)
            {
                Debug.LogError("Przekroczono limit iteracji w pêtli WHILE. SprawdŸ warunek: " + instr.Condition);
                break;
            }
        }
    }
    private float EvaluateNumericExpression(string expr)
    {
        expr = expr.Trim();

        //zmienna
        if (NumberVars.TryGetValue(expr, out float value))
            return value;
        //liczba
        if (float.TryParse(expr, System.Globalization.NumberStyles.Float,
                      System.Globalization.CultureInfo.InvariantCulture,
                      out float literal))
            return literal;

        if (expr.Contains('+'))
        {
            var parts = expr.Split('+');
            return GetNumberValue(parts[0] + GetNumberValue(parts[1]));
        }

        if (expr.Contains('-'))
        {
            var parts = expr.Split('-');
            return GetNumberValue(parts[0] + GetNumberValue(parts[1]));
        }

        Debug.LogError("Nieznane wyrazenie liczbowe: " + expr);
        return 0f;
    }
    private void ExecuteAssignment(ParsedInstruction instr)
    {
        string variableName = instr.Name;
        string expression = instr.Arguments[0];

        //bool
        if(bool.TryParse(expression, out bool boolValue))
        {
            BoolVars[variableName] = boolValue;
            return;
        }

        //number
        float numberValue = EvaluateNumericExpression(expression);
        NumberVars[variableName] = numberValue;
    }


}
