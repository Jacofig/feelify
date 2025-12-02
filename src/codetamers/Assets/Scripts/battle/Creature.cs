using UnityEngine;

public class Creature : MonoBehaviour
{
    public PokemonData data;

    public int currentHP;

    void Start()
    {
        currentHP = data.maxHP;
        GetComponent<SpriteRenderer>().sprite = data.battleSprite;
    }
}
