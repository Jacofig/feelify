[System.Serializable]
public class PlayerPokemon
{
    public PokemonData data;

    public int level;
    public int currentXP;
    public int currentHP;
    public int currentMana;

    public PlayerPokemon(PokemonData data)
    {
        this.data = data;
        level = data.level;
        currentHP = data.maxHP;
        currentMana = data.startingMana;
        currentXP = 0;
    }

    public int MaxHP => data.maxHP + level * 2;
    public int Attack => data.attack + level;
    public int Defense => data.defense + level;

}
