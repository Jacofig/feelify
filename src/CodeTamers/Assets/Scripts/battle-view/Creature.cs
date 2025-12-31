using UnityEngine;
public class Creature : MonoBehaviour
{
    public PokemonData data;
    public int currentHP;
    public int currentMana;

    public string codeBuffer = ""; // <-- each pokemon has its own code

    public void Init(PokemonData newData)
    {
        data = newData;
        currentHP = data.maxHP;
        currentMana = Mathf.Clamp(data.startingMana, 0, data.maxMana);

        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.sprite = data.battleSprite;
    }
}
