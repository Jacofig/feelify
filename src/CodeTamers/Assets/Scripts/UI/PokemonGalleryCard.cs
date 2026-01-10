using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PokemonGalleryCard : MonoBehaviour
{
    public Image sprite;
    public TMP_Text nameText;
    public TMP_Text levelText;

    private PokemonInstance pokemon;
    private PokedexUI pokedexUI;

    public void Setup(PokemonInstance pokemon, PokedexUI ui)
    {
        this.pokemon = pokemon;
        this.pokedexUI = ui;

        sprite.sprite = pokemon.data.battleSprite;
        nameText.text = pokemon.data.pokemonName;
        levelText.text = "Lv " + pokemon.level;
    }

    public void OnClick()
    {
        pokedexUI.ShowDetails(pokemon);
    }
}
