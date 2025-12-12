using System;
using System.Collections.Generic;

public class SimpleParser
{
    private const int INDENT_SIZE = 4;

    public List<ParsedInstruction> Parse(string code)
    {
        // lista instrukcji na poziomie 0 (bez wciêæ)
        List<ParsedInstruction> rootInstructions = new List<ParsedInstruction>();

        // stos bloków, np. if-y, w które wchodzimy
        Stack<ParsedInstruction> blockStack = new Stack<ParsedInstruction>();

        int previousIndentLevel = 0;

        // Normalizacja koñcówek linii
        code = code.Replace("\r\n", "\n");
        string[] lines = code.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            string rawLine = lines[i];

            // pomijamy linie puste / z samymi spacjami
            if (string.IsNullOrWhiteSpace(rawLine))
                continue;

            // LICZENIE WCIÊÆ (SPACJI NA POCZ¥TKU)
            int indentSpaces = CountIndent(rawLine);

            if (indentSpaces % INDENT_SIZE != 0)
            {
                throw new Exception($"B³¹d wciêcia w linii {i + 1}: wciêcie musi byæ wielokrotnoœci¹ {INDENT_SIZE} spacji.");
            }

            int indentLevel = indentSpaces / INDENT_SIZE;

            // wycinamy lewe/prawe spacje – zostaje sama treœæ kodu
            string line = rawLine.Trim();

            // PARSOWANIE TEJ KONKRETNEJ LINII DO ParsedInstruction
            ParsedInstruction instr = ParseSingleLine(line, i + 1);

            // zapamiêtujemy poziom wciêcia w instrukcji
            instr.IndentLevel = indentLevel;

            // USTALAMY, DO KOGO TA INSTRUKCJA NALE¯Y (DRZEWO BLOKÓW)

            if (blockStack.Count == 0)
            {
                // nie jesteœmy w ¿adnym bloku
                if (indentLevel != 0)
                {
                    throw new Exception($"Nieoczekiwane wciêcie w linii {i + 1}: kod nie mo¿e zaczynaæ siê od wciêcia.");
                }

                rootInstructions.Add(instr);
            }
            else
            {
                // jesteœmy w jakimœ bloku (np. w if-ie)
                while (blockStack.Count > 0 && indentLevel <= blockStack.Peek().IndentLevel)
                {
                    // wychodzimy z bloków, dopóki aktualne wciêcie
                    // nie bêdzie wiêksze od wciêcia bloku na stosie
                    blockStack.Pop();
                }

                if (blockStack.Count == 0)
                {
                    // wróciliœmy na poziom 0
                    if (indentLevel != 0)
                    {
                        throw new Exception($"B³êdne wciêcie w linii {i + 1}: brak pasuj¹cego bloku nadrzêdnego.");
                    }

                    rootInstructions.Add(instr);
                }
                else
                {
                    // instrukcja jest dzieckiem ostatniego bloku
                    blockStack.Peek().Children.Add(instr);
                }
            }

            // jeœli ta instrukcja sama tworzy blok (np. if),
            // to odk³adamy j¹ na stos, ¿eby nastêpne wciête linie
            // trafi³y do jej Children
            if (instr.Type == InstructionType.If || instr.Type == InstructionType.While)
            {
                blockStack.Push(instr);
            }

            previousIndentLevel = indentLevel;
        }

        return rootInstructions;
    }

    // liczy spacje od pocz¹tku linii
    private int CountIndent(string line)
    {
        int count = 0;
        foreach (char c in line)
        {
            if (c == ' ')
                count++;
            else
                break;
        }
        return count;
    }

    // parsuje pojedyncz¹ liniê: if ..., attack(), block() itd.
    private ParsedInstruction ParseSingleLine(string line, int lineNumber)
    {
        // 1. IF
        if (line.StartsWith("if") && line.EndsWith(":"))
        {
            // wycinamy "if" z przodu, ":" z koñca
            string condition = line.Substring(2).Trim(); // po "if"
            condition = condition.TrimEnd(':').Trim();   // bez ":" na koñcu

            return new ParsedInstruction
            {
                Type = InstructionType.If,
                Condition = condition,
                LineNumber = lineNumber
            };
        }

        // WHILE
        if (line.StartsWith("while") && line.EndsWith(":"))
        {
            string condition = line.Substring(5).Trim(); // po "while"
            condition = condition.TrimEnd(':').Trim();

            return new ParsedInstruction
            {
                Type = InstructionType.While,
                Condition = condition,
                LineNumber = lineNumber
            };
        }
        // 2. KOMENDA GRY (attack(), block(), itd.)

        // musi mieæ nawiasy
        if (!line.Contains("(") || !line.EndsWith(")"))
        {
            string nameTmp = ExtractName(line);
            throw new Exception($"B³¹d sk³adni: komenda '{nameTmp}' musi mieæ nawiasy () w linii {lineNumber}");
        }

        string name = ExtractName(line);
        string[] args = ExtractArguments(line);

        if (GameCommandLibrary.IsGameCommand(name))
        {
            return GameCommandLibrary.Create(name, args, lineNumber);
        }

        throw new Exception($"Nieznana instrukcja '{line}' w linii {lineNumber}");
    }

    // Wyci¹ga nazwê komendy: attack() -> "attack"
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
