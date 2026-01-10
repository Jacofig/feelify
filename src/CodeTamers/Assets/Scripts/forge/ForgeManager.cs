using UnityEngine;
using System.Collections.Generic;

public class ForgeManager : MonoBehaviour
{
    public ForgeInstructionInterpreter interpreter;
    public ForgeActionExecutor executor = new();

    public ForgeRecipeSO activeRecipe;

    // metal startowy (np. iron)
    public Metal startingMetal;

    public ForgeRecipeDatabase recipeDatabase;
    [SerializeField]
    private int testRecipeIndex = 0;


    public void RunForge(string code)
    {
        if (recipeDatabase == null || recipeDatabase.recipes.Count == 0)
        {
            Debug.LogError("No recipe database or empty!");
            return;
        }

        activeRecipe = recipeDatabase.recipes[testRecipeIndex];

        // 1. Parser
        var instructions = new SimpleParser().Parse(code);

        // 2. Context (tylko podgl¹d)
        var context = new ForgeContext
        {
            temperature = startingMetal.temperature,
            hits = startingMetal.hits,
            enchanted = startingMetal.enchanted
        };

        // 3. Interpreter
        var actions = interpreter.Execute(context, instructions);
        if (actions == null)
        {
            Debug.Log("Forge failed: code error");
            return;
        }

        // 4. Proces
        var metal = CloneMetal(startingMetal);
        var process = new ForgeProcess { metal = metal };

        foreach (var action in actions)
        {
            if (!executor.Execute(action, process))
            {
                Debug.Log($"Forge failed: {process.failReason}");
                return;
            }
        }

        // 5. Receptura
        bool success = activeRecipe.Validate(process);
        Debug.Log(success ? "FORGE SUCCESS" : "FORGE FAILED");
    }


    private Metal CloneMetal(Metal src)
    {
        return new Metal
        {
            id = src.id,
            minHitTemp = src.minHitTemp,
            maxTemp = src.maxTemp,
            temperature = src.temperature,
            hits = src.hits,
            enchanted = src.enchanted
        };
    }
    void Start()
    {
        // =========================
        // TESTOWY METAL (wspólny)
        // =========================
        startingMetal = new Metal
        {
            id = "copper",
            minHitTemp = 25,     // 1 heat (+50) = 2 hity
            maxTemp = 200,
            temperature = 0,
            hits = 0,
            enchanted = false
        };

        Debug.Log("=== TEST 1: BEZ IF (powinien FAIL, bo brak heat) ===");
        RunForge(
    @"hit()
hit()
cast()"
        );

        Debug.Log("=== TEST 2: IF + HEAT (powinien SUCCESS) ===");
        RunForge(
    @"if get_metal_temp() < 25:
    heat()
hit()
hit()
cast()"
        );

        Debug.Log("=== TEST 3: IF + ZA MA£O HITÓW (powinien FAIL receptury) ===");
        RunForge(
    @"if get_metal_temp() < 25:
    heat()
hit()
cast()"
        );

        Debug.Log("=== TEST 4: ZAWSZE HEAT (powinien SUCCESS) ===");
        RunForge(
    @"heat()
hit()
hit()
cast()"
        );

        Debug.Log("=== TEST 5: WHILE + IF (powinien SUCCESS) ===");
        RunForge(
        @"i = 0
while i < 2:
    if get_metal_temp() < 25:
        heat()
    hit()
    i = i + 1
cast()"
        );

    }



}
