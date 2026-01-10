using UnityEngine;

[System.Serializable]
public class PokemonInstance
{
    public PokemonData data;

    public int level;
    public int currentXP;
    public int currentHP;
    public int currentMana;

    public PokemonInstance(PokemonData data)
    {
        this.data = data;

        level = data.level;
        currentXP = 0;
        currentHP = MaxHP;
        currentMana = Mathf.Clamp(data.startingMana, 0, data.maxMana);
    }

    public int MaxHP => data.maxHP + level * 2;
    public int Attack => data.attack + level;
    public int Defense => data.defense + level;
}
