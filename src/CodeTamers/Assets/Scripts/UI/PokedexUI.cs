using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PokedexUI : MonoBehaviour
{
    [Header("Data")]
    public PlayerParty playerParty;

    [Header("Gallery")]
    public GameObject galleryView;
    public Transform galleryContent;
    public GameObject pokemonCardPrefab;

    [Header("Detail View")]
    public GameObject detailView;
    public Image bigSprite;
    public TMP_Text nameText;
    public TMP_Text levelText;
    public Slider hpBar;
    public Slider manaBar;
    public TMP_Text statsText;

    private PokemonInstance currentPokemon;

    void OnEnable()
    {
        if (playerParty == null)
            playerParty = PlayerParty.Instance;

        ShowGallery();
    }

    void ShowGallery()
    {
        galleryView.SetActive(true);
        detailView.SetActive(false);

        foreach (Transform child in galleryContent)
            Destroy(child.gameObject);

        foreach (PokemonInstance p in playerParty.collection)
        {
            GameObject card = Instantiate(pokemonCardPrefab, galleryContent);
            card.GetComponent<PokemonGalleryCard>().Setup(p, this);
        }
    }

    public void ShowDetails(PokemonInstance pokemon)
    {
        currentPokemon = pokemon;

        galleryView.SetActive(false);
        detailView.SetActive(true);

        bigSprite.sprite = pokemon.data.battleSprite;
        nameText.text = pokemon.data.pokemonName;
        levelText.text = "Lv " + pokemon.level;

        hpBar.maxValue = pokemon.MaxHP;
        hpBar.value = pokemon.currentHP;

        manaBar.maxValue = pokemon.data.maxMana;
        manaBar.value = pokemon.currentMana;

        statsText.text =
            $"ATK: {pokemon.Attack}\n" +
            $"DEF: {pokemon.Defense}\n" +
            $"SPD: {pokemon.data.speed}";
    }

    public void BackToGallery()
    {
        ShowGallery();
    }
}
