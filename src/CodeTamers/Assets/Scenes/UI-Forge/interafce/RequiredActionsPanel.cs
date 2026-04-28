using UnityEngine;
using TMPro;

public class RequiredActionsPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text actionsText;

    public void Show(ForgeRecipeSO recipe)
    {
        string result = "";

        if (recipe.requiredHits > 0)
            result += $"hit() x{recipe.requiredHits}\n";

        if (recipe.requiresEnchant)
            result += "enchant() x1\n";

        actionsText.text = result;
    }
}
