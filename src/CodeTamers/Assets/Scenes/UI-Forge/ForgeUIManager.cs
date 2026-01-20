using UnityEngine;

public class ForgeUIManager : MonoBehaviour
{
    [Header("Database")]
    [SerializeField] private ForgeRecipeDatabase recipeDatabase;

    [Header("UI")]
    [SerializeField] private Transform contentParent;
    [SerializeField] private RecipeTileUI recipeTilePrefab;

    [Header("Panels")]
    [SerializeField] private RequiredActionsPanel actionsPanel;
    [SerializeField] private RequiredItemsPanel itemsPanel;

    [SerializeField] private ForgeManager forgeManager;

    private ForgeRecipeSO selectedRecipe;

    private void Start()
    {
        GenerateRecipeList();
    }

    private void OnRecipeSelected(ForgeRecipeSO recipe)
    {
        Debug.Log("SELECTED RECIPE: " + recipe.recipeId);
    }


    private void GenerateRecipeList()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        foreach (var recipe in recipeDatabase.recipes)
        {
            var tile = Instantiate(recipeTilePrefab, contentParent);
            tile.Setup(recipe, OnRecipeSelected);
            tile.Setup(recipe, OnRecipeClicked);
        }
    }

    private void OnRecipeClicked(ForgeRecipeSO recipe)
    {
        selectedRecipe = recipe;

        if (forgeManager != null)
            forgeManager.activeRecipe = recipe;

        actionsPanel.Show(recipe);
        itemsPanel.Show(recipe);

        Debug.Log("ACTIVE RECIPE SET IN FORGE: " + recipe.recipeId);
    }


}
