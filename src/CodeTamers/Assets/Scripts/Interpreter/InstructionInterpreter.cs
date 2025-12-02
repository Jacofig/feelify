using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InstructionInterpreter : MonoBehaviour
{
    public PlayerSpeechBubble playerBubble;

    private float displayTime = 1.2f; // ile czasu dymek jest widoczny dla ka¿dej komendy

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

    private IEnumerator ExecuteIf(ParsedInstruction instr)
    {
        foreach (var child in instr.Children)
        {
            yield return ExecuteInstruction(child);
        }
    }
}
