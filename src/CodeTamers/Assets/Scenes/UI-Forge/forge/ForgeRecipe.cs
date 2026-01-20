using System.Collections.Generic;

public class ForgeRecipe
{
    public string recipeId;
    public string requiredMetalId;
    public int requiredHits;
    public string requiredAdditive;
    public bool requiresEnchant;

    public bool Validate(ForgeProcess process)
    {
        var metal = process.metal;

        if (metal.id != requiredMetalId) return false;
        if (metal.hits < requiredHits) return false;
        if (!metal.additives.Contains(requiredAdditive)) return false;
        if (requiresEnchant && !metal.enchanted) return false;
        if (requiresEnchant && metal.hits < requiredHits)
            return false;

        return ValidateOrder(process.executedActions);
    }

    private bool ValidateOrder(List<ForgeAction> actions)
    {
        bool seenHit = false;
        bool seenAdd = false;

        foreach (var a in actions)
        {
            if (a.Type == ForgeActionType.Hit)
                seenHit = true;

            if (a.Type == ForgeActionType.Add && !seenHit)
                return false;

            if (a.Type == ForgeActionType.Cast && !seenAdd)
                return false;

            if (a.Type == ForgeActionType.Add)
                seenAdd = true;
          
        }

        return true;
    }
}
