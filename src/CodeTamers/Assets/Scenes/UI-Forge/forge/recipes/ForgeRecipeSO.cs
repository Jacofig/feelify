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

    [Header("Output")]
    public ItemData outputItem;
    public int outputAmount = 1;

    public bool Validate(ForgeProcess process)
    {
        if (process.failed)
            return false;

        var metal = process.metal;

        // 1. Hity
        if (metal.hits < requiredHits)
            return false;

        // 2. Enchant
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