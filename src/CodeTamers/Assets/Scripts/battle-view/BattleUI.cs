using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [Header("Player UI")]
    public TMP_Text playerNameText;
    public TMP_Text playerLevelText;
    public Slider playerHPBar;

    [Header("Enemy UI")]
    public TMP_Text enemyNameText;
    public TMP_Text enemyLevelText;
    public Slider enemyHPBar;

    public void SetPlayerUI(Creature player)
    {
        playerNameText.text = player.data.pokemonName;
        playerLevelText.text = "Lv. " + player.data.level;

        playerHPBar.maxValue = player.data.maxHP;
        playerHPBar.value = player.currentHP;
    }

    public void SetEnemyUI(Creature enemy)
    {
        enemyNameText.text = enemy.data.pokemonName;
        enemyLevelText.text = "Lv. " + enemy.data.level;

        enemyHPBar.maxValue = enemy.data.maxHP;
        enemyHPBar.value = enemy.currentHP;
    }

    public void UpdateEnemyHP(Creature enemy)
    {
        enemyHPBar.value = enemy.currentHP;
    }


}
