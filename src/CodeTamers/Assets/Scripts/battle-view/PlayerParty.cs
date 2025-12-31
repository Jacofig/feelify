using UnityEngine;

public class PlayerParty : MonoBehaviour
{
    public static PlayerParty Instance { get; private set; }

    [Header("Exactly 3 for now")]
    public PokemonData[] team = new PokemonData[3];

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
