using UnityEngine;
using System.Collections.Generic;

public class PlayerParty : MonoBehaviour
{
    public static PlayerParty Instance { get; private set; }

    [Header("Starting Pokémon (templates)")]
    public PokemonData[] startingPokemon;

    [Header("Active Party (always 3)")]
    public PokemonInstance[] party = new PokemonInstance[3];

    [Header("All owned Pokémon")]
    public List<PokemonInstance> collection = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Initialize();
    }

    void Initialize()
    {
        // Always enforce party size = 3
        party = new PokemonInstance[3];
        collection.Clear();

        // Build collection from templates
        foreach (PokemonData data in startingPokemon)
        {
            if (data == null)
                continue;

            collection.Add(new PokemonInstance(data));
        }

        // Take first 3 owned Pokémon into party
        for (int i = 0; i < party.Length; i++)
        {
            if (i < collection.Count)
                party[i] = collection[i];
            else
                party[i] = null;
        }
    }

    public void AddPokemon(PokemonData data)
    {
        if (data == null) return;

        PokemonInstance instance = new PokemonInstance(data);
        collection.Add(instance);

        // Optional: auto-fill empty party slot
        for (int i = 0; i < party.Length; i++)
        {
            if (party[i] == null)
            {
                party[i] = instance;
                break;
            }
        }
    }
}
