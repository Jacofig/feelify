using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(
    fileName = "ForgeRecipe",
    menuName = "Forge/Recipe"
)]
public class ForgeRecipeSO : ScriptableObject
{
    [Header("ID")]
    public string recipeId;

    [Header("UI")]
    public string displayName;

    [Header("Metal")]
    public string requiredMetalId;

    [Header("Shaping")]
    public int requiredHits;

    [Header("Required Items")]
    public List<RecipeItemRequirement> requiredItems;

    [Header("Enchant")]
    public bool requiresEnchant;

    [Header("Tutorial")]
    public bool tutorialOnly;

    [Header("Tutorial")]
    public string tutorialStepId;


    public bool Validate(ForgeProcess process)
    {
        if (process.failed)
            return false;

        var metal = process.metal;

        // 1. Metal
        if (metal.id != requiredMetalId)
            return false;

        // 2. Hity
        if (metal.hits < requiredHits)
            return false;

        // 3. Itemy (add)
        foreach (var req in requiredItems)
        {
            if (!metal.additives.Contains(req.itemId))
                return false;
        }

        // 4. Enchant – MUSI BYÆ I MUSI BYÆ NA KOÑCU
        if (requiresEnchant)
        {
            if (!metal.enchanted)
                return false;

            if (process.executedActions.Count == 0)
                return false;

            if (process.executedActions[^1].Type != ForgeActionType.Cast)
                return false;
        }

        return true;
    }



}
