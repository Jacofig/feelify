using UnityEngine;

public class Creature : MonoBehaviour
{
    public PokemonData data;

    public int currentHP;

    public void TakeDamage(int amount)
    {
        currentHP = Mathf.Max(0, currentHP - amount);
    }
    void Start()
    {
        currentHP = data.maxHP;
        GetComponent<SpriteRenderer>().sprite = data.battleSprite;
    }
}
