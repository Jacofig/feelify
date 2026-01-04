using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public PokemonData data;
    public int currentHP;
    public int currentMana;
    public int teamIndex;
    public string codeBuffer = "";

    
    public List<StatusEffect> statusEffects = new();

    public void Init(PokemonData newData)
    {
        data = newData;
        currentHP = data.maxHP;
        currentMana = Mathf.Clamp(data.startingMana, 0, data.maxMana);

        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.sprite = data.battleSprite;
    }

    
    public void AddStatus(StatusEffect status)
    {
        status.owner = this;
        statusEffects.Add(status);
        status.OnApply();
    }

    public void OnTurnStart()
    {
        for (int i = statusEffects.Count - 1; i >= 0; i--)
        {
            var status = statusEffects[i];

            if (status.ExpireOnOwnerTurnStart)
            {
                status.OnRemove();
                statusEffects.RemoveAt(i);
            }
        }
    }
}
