using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Transform playerSpawn;
    public Transform enemySpawn;

    public GameObject playerCreaturePrefab;
    public GameObject enemyCreaturePrefab;

    public BattleUI battleUI;

    private Creature player;
    private Creature enemy;

    public void PlayerAttack()
    {
        enemy.TakeDamage(1);
        battleUI.UpdateEnemyHP(enemy);
    }

    public void PlayerBlock()
    {
        Debug.Log("Player blocks");
    }

    void Start()
    {
        var playerObj = Instantiate(playerCreaturePrefab, playerSpawn.position, Quaternion.identity);
        var enemyObj = Instantiate(enemyCreaturePrefab, enemySpawn.position, Quaternion.identity);

        player = playerObj.GetComponent<Creature>();
        enemy = enemyObj.GetComponent<Creature>();

        player.data = BattleData.playerData;
        enemy.data = BattleData.enemyData;

        battleUI.SetPlayerUI(player);
        battleUI.SetEnemyUI(enemy);
    }
}
