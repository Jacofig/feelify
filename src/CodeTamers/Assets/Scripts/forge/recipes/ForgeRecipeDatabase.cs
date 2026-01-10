using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(
    fileName = "ForgeRecipeDatabase",
    menuName = "Forge/Recipe Database"
)]
public class ForgeRecipeDatabase : ScriptableObject
{
    public List<ForgeRecipeSO> recipes;
}
