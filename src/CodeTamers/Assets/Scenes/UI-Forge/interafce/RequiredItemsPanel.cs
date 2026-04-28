using UnityEngine;
using TMPro;

public class RequiredItemsPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text itemsText;

    public void Show(ForgeRecipeSO recipe)
    {
        string text = "";

        foreach (var req in recipe.requiredItems)
        {
            int playerAmount = PlayerInventory.Instance.GetItemCount(req.itemId);

            text += $"{req.itemId} {playerAmount}/{req.amount}\n";
        }

        itemsText.text = text;
    }
}
