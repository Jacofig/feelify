using UnityEngine;
using System.Collections.Generic;

public class ForgeManager : MonoBehaviour
{
    public ForgeInstructionInterpreter interpreter;
    public ForgeActionExecutor executor = new();

    public ForgeRecipeSO activeRecipe;

    [SerializeField]
    private Metal startingMetal;

    public ForgeRecipeDatabase recipeDatabase;

    [SerializeField]
    private bool runDebugTests = false;

    public System.Action<ForgeResultType> OnForgeResult;
    public string lastErrorMessage { get; private set; }


    public void RunForge(string code)
    {
        if (startingMetal == null)
        {
            Debug.LogError("Starting metal not set!");
            return;
        }

        if (recipeDatabase == null || recipeDatabase.recipes.Count == 0)
        {
            Debug.LogError("No recipe database or empty!");
            return;
        }

        if (activeRecipe == null)
        {
            Debug.LogError("No active recipe set!");
            return;
        }


        // 1. Parser
        List<ParsedInstruction> instructions;

        try
        {
            instructions = new SimpleParser().Parse(code);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("FORGE CODE ERROR: " + e.Message);
            lastErrorMessage = e.Message;
            OnForgeResult?.Invoke(ForgeResultType.CodeError);
            return;
        }



        // 2. Context (tylko podgl¹d)
        var context = new ForgeContext
        {
            temperature = startingMetal.temperature,
            hits = startingMetal.hits,
            enchanted = startingMetal.enchanted
        };

        // 3. Interpreter
        // 3. Interpreter
        List<ForgeAction> actions;

        try
        {
            actions = interpreter.Execute(context, instructions);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("FORGE RUNTIME ERROR: " + e.Message);

            lastErrorMessage = e.Message;

            OnForgeResult?.Invoke(ForgeResultType.CodeError);
            return;
        }

        if (actions == null)
        {
            Debug.Log("Forge failed: interpreter error");

            lastErrorMessage = "Interpreter error";

            OnForgeResult?.Invoke(ForgeResultType.CodeError);
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

                if (process.failReason == "Hit on cold metal")
                    OnForgeResult?.Invoke(ForgeResultType.HitColdMetal);
                else
                    OnForgeResult?.Invoke(ForgeResultType.Failed);

                return;
            }
        }


        if (TutorialManager.ForgeTutorialActive)
        {
            var controller = FindObjectOfType<ForgeTutorialController>();

            if (controller != null &&
                activeRecipe.tutorialStepId != controller.GetCurrentStepId())
            {
                lastErrorMessage = "Wrong tutorial recipe";
                OnForgeResult?.Invoke(ForgeResultType.Failed);
                return;
            }
        }

        // 5. Receptura
        bool success = activeRecipe.Validate(process);
        Debug.Log(success ? "FORGE SUCCESS" : "FORGE FAILED");
        OnForgeResult?.Invoke(
        success ? ForgeResultType.Success : ForgeResultType.Failed
        );
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
    void Awake()
    {
        if (startingMetal == null)
        {
            startingMetal = new Metal
            {
                id = "copper",
                minHitTemp = 25,
                maxTemp = 200,
                temperature = 0,
                hits = 0,
                enchanted = false
            };
        }
    }


    void Start()
    {
        if (!runDebugTests)
            return;
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
