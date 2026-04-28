using UnityEngine;
using TMPro;

public class RequiredItemsPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text itemsText;

    public void Show(ForgeRecipeSO recipe)
    {
        string result = "";

        result += $"Metal: {recipe.requiredMetalId}\n";

        if (recipe.requiredItems != null && recipe.requiredItems.Count > 0)
        {
            foreach (var item in recipe.requiredItems)
            {
                result += $"{item.itemId} x{item.amount}\n";
            }
        }

        itemsText.text = result;
    }
}
