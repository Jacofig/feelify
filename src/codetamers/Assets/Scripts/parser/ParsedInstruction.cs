
using System.Collections.Generic;

public class ParsedInstruction
{
    public InstructionType Type;
    public string Name;
    public string[] Arguments;
    public string Condition;
    public List<ParsedInstruction> Children = new List<ParsedInstruction>();
    public int LineNumber;
}
