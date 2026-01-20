using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeTileUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Button button;

    private ForgeRecipeSO recipe;

    public void Setup(ForgeRecipeSO recipeData, System.Action<ForgeRecipeSO> onClick)
    {
        recipe = recipeData;
        nameText.text = recipe.displayName;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            Debug.Log("CLICKED TILE: " + recipe.recipeId);
            onClick?.Invoke(recipe);
        });
    }
}
