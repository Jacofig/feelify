using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Transform playerSpawn;
    public Transform enemySpawn;

    public GameObject playerCreaturePrefab;
    public GameObject enemyCreaturePrefab;

    public BattleUI battleUI;

    void Start()
    {
        var playerObj = Instantiate(playerCreaturePrefab, playerSpawn.position, Quaternion.identity);
        var enemyObj = Instantiate(enemyCreaturePrefab, enemySpawn.position, Quaternion.identity);

        var player = playerObj.GetComponent<Creature>();
        var enemy = enemyObj.GetComponent<Creature>();

        player.data = BattleData.playerData;
        enemy.data = BattleData.enemyData;

        // Update UI
        battleUI.SetPlayerUI(player);
        battleUI.SetEnemyUI(enemy);
    }
}
