using System.Collections.Generic;
using UnityEngine;

public class ForgeInstructionInterpreter : MonoBehaviour
{
    public InstructionInterpreter baseInterpreter;
    public ForgeCommandHandler forgeHandler;

    public List<ForgeAction> Execute(
        ForgeContext ctx,
        List<ParsedInstruction> instructions
    )
    {
        Debug.Log("Interpreter received context:");
        Debug.Log($"temp={ctx.temperature}, hits={ctx.hits}, enchanted={ctx.enchanted}");

        // resetujemy akcje z poprzedniego uruchomienia
        forgeHandler.ResetActions();

        // podpinamy handler komend (heat, hit, cast, add)
        baseInterpreter.commandHandler = forgeHandler;

        // =========================
        // ZMIENNE DOSTÊPNE DLA GRACZA
        // =========================
        baseInterpreter.NumberVars["temperature"] = ctx.temperature;
        baseInterpreter.NumberVars["hits"] = ctx.hits;
        baseInterpreter.BoolVars["enchanted"] = ctx.enchanted;

        // =========================
        // FUNKCJE DOSTÊPNE DLA GRACZA
        // =========================
        // python-like: get_metal_temp()
        baseInterpreter.NumberFunctions["get_metal_temp"] = () =>
        {
            return ctx.temperature;
        };

        // =========================
        // WYKONANIE KODU GRACZA
        // =========================
        bool ok = baseInterpreter.Execute(instructions);
        if (!ok)
            return null;

        // zwracamy listê intencji (ForgeAction)
        return forgeHandler.GetActions();
    }
}
