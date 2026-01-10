using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyEncounter : MonoBehaviour
{
    private bool playerInRange = false;

    [Header("Enemy team (templates)")]
    public PokemonData[] enemyTeam = new PokemonData[3];

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Press E to start battle");
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            playerInRange = false;
    }

    private void Update()
    {
        if (!playerInRange) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            // PLAYER: runtime instances
            BattleData.playerTeam = PlayerParty.Instance.party;

            // ENEMY: templates -> instances
            BattleData.enemyTeam = BuildEnemyTeam();

            Debug.Log("=== Enemy team set ===");
            for (int i = 0; i < BattleData.enemyTeam.Length; i++)
            {
                Debug.Log(
                    BattleData.enemyTeam[i] != null
                        ? BattleData.enemyTeam[i].data.pokemonName
                        : "NULL"
                );
            }

            SceneManager.LoadScene("BattleScene");
        }
    }

    // ✅ FINAL correct version
    private PokemonInstance[] BuildEnemyTeam()
    {
        PokemonInstance[] result = new PokemonInstance[enemyTeam.Length];

        for (int i = 0; i < enemyTeam.Length; i++)
        {
            if (enemyTeam[i] == null)
                continue;

            result[i] = new PokemonInstance(enemyTeam[i]);
        }

        return result;
    }
}
