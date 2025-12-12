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
    [Header("Graphics")]
    public Sprite battleSprite;

    [Header("Other")]
    public string description;
}
