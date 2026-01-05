using UnityEngine;

[CreateAssetMenu(fileName = "NewPokemon", menuName = "Pokemon/Create New Pokemon")]
public class PokemonData : ScriptableObject
{
    public string pokemonName;
    

    [Header("Stats")]
    public int maxHP;
    public int attack;
    public int defense;
    public int speed;
    public int level;

    [Header("Mana")]
    public int maxMana = 10;
    public int manaRegenPerTurn = 1;     // optional, useful later
    public int startingMana = 10;        // optional (can be = maxMana)

    [Header("Graphics")]
    public Sprite battleSprite;

    [Header("Other")]
    public string description;

    public AnimatorOverrideController animatorOverride;

}
