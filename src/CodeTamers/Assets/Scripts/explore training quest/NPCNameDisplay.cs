using TMPro;
using UnityEngine;

public class NPCNameDisplay : MonoBehaviour
{
    public PokemonData pokemonData;
    public TextMeshPro text;   // <-- TO jest poprawna zmienna

    void Start()
    {
        if (pokemonData != null && text != null)
        {
            text.text = $"{pokemonData.pokemonName} Lv.{pokemonData.level}";
        }
    }
}
