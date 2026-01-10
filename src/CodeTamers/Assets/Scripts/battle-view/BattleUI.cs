using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class BattleUI : MonoBehaviour
{
    [System.Serializable]
    public class BattleUnitUI
    {
        public TMP_Text nameText;
        public TMP_Text levelText;
        public TMP_Text hpText;     // HP as text
        public TMP_Text manaText;   // Mana as text

        public Image hpBarFill;
        public Image manaBarFill;
    }

    [Header("Player Team UI (size = 3)")]
    public BattleUnitUI[] playerSlots = new BattleUnitUI[3];

    [Header("Enemy Team UI (size = 3)")]
    public BattleUnitUI[] enemySlots = new BattleUnitUI[3];

    // ===== PUBLIC API =====

    public void SetPlayerTeam(List<Creature> players)
    {
        for (int i = 0; i < playerSlots.Length; i++)
        {
            if (i < players.Count)
                SetSlot(playerSlots[i], players[i]);
            else
                ClearSlot(playerSlots[i]);
        }
    }

    public void SetEnemyTeam(List<Creature> enemies)
    {
        for (int i = 0; i < enemySlots.Length; i++)
        {
            if (i < enemies.Count)
                SetSlot(enemySlots[i], enemies[i]);
            else
                ClearSlot(enemySlots[i]);
        }
    }

    public void UpdateSinglePlayer(int index, Creature creature)
    {
        if (index < 0 || index >= playerSlots.Length) return;
        SetSlot(playerSlots[index], creature);
    }

    public void UpdateSingleEnemy(int index, Creature creature)
    {
        if (index < 0 || index >= enemySlots.Length) return;
        SetSlot(enemySlots[index], creature);
    }

    // ===== INTERNAL =====

    private void SetSlot(BattleUnitUI ui, Creature c)
    {
        if (c == null || c.instance == null)
        {
            ClearSlot(ui);
            return;
        }

        var inst = c.instance;
        var data = inst.data;

        ui.nameText.text = data.pokemonName;
        ui.levelText.text = $"Lv. {inst.level}";

        ui.hpText.text = $"{c.CurrentHP} / {inst.MaxHP}";
        ui.manaText.text = $"{c.CurrentMana} / {data.maxMana}";

        ui.hpBarFill.fillAmount =
            inst.MaxHP > 0 ? (float)c.CurrentHP / inst.MaxHP : 0f;

        ui.manaBarFill.fillAmount =
            data.maxMana > 0 ? (float)c.CurrentMana / data.maxMana : 0f;
    }



    private void ClearSlot(BattleUnitUI ui)
    {
        ui.nameText.text = "---";
        ui.levelText.text = "";
        ui.hpText.text = "-- / --";
        ui.manaText.text = "-- / --";

        ui.hpBarFill.fillAmount = 0f;
        ui.manaBarFill.fillAmount = 0f;
    }

}
