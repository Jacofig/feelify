using System;
using System.Collections.Generic;

public static class GameCommandLibrary
{
    public delegate ParsedInstruction CommandHandler(string[] args, int lineNumber);

    public static readonly Dictionary<string, CommandHandler> Commands =
        new Dictionary<string, CommandHandler>
    {
        {
            "attack", (args, line) => new ParsedInstruction
            {
                Type = InstructionType.GameCommand,
                Name = "attack",
                Arguments = args,
                LineNumber = line
            }
        },
        {
            "block", (args, line) => new ParsedInstruction
            {
                Type = InstructionType.GameCommand,
                Name = "block",
                Arguments = args,
                LineNumber = line
            }
        }
    };

    public static bool IsGameCommand(string name)
    {
        return Commands.ContainsKey(name);
    }

    public static ParsedInstruction Create(string name, string[] args, int line)
    {
        if (!Commands.ContainsKey(name))
            throw new Exception($"Nieznana komenda gry '{name}' w linii {line}");

        return Commands[name](args, line);
    }
}
