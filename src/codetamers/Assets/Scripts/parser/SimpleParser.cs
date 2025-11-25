using System;
using System.Collections.Generic;

public class SimpleParser
{
    public List<ParsedInstruction> Parse(string code)
    {
        List<ParsedInstruction> instructions = new List<ParsedInstruction>();

        // Normalizacja
        code = code.Replace("\r\n", "\n");
        string[] lines = code.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            string rawLine = lines[i];
            string line = rawLine.Trim();

            if (string.IsNullOrWhiteSpace(line))
                continue;

            // 1. Sprawdzenie IF
            if (line.StartsWith("if ") && line.EndsWith(":"))
            {
                string condition = line.Substring(3, line.Length - 4).Trim();

                ParsedInstruction ifInstruction = new ParsedInstruction
                {
                    Type = InstructionType.If,
                    Condition = condition,
                    LineNumber = i + 1
                };

                instructions.Add(ifInstruction);
                continue;
            }

            // 2. Komendy gry (attack, block ...)
            string name = ExtractName(line);
            string[] args = ExtractArguments(line);

            if (GameCommandLibrary.IsGameCommand(name))
            {
                ParsedInstruction instr =
                    GameCommandLibrary.Create(name, args, i + 1);

                instructions.Add(instr);
            }
            else
            {
                throw new Exception($"Nieznana instrukcja '{line}' w linii {i + 1}");
            }
        }

        return instructions;
    }

    // Wyci¹ga nazwê komendy: attack() -> attack
    private string ExtractName(string line)
    {
        int index = line.IndexOf('(');
        if (index == -1) return line;
        return line.Substring(0, index);
    }

    // Wyci¹ga argumenty: move(3,4) -> ["3","4"]
    private string[] ExtractArguments(string line)
    {
        int open = line.IndexOf('(');
        int close = line.LastIndexOf(')');

        if (open == -1 || close == -1)
            return new string[0];

        string content = line.Substring(open + 1, close - open - 1);

        if (string.IsNullOrWhiteSpace(content))
            return new string[0];

        return content.Split(',');
    }
}
