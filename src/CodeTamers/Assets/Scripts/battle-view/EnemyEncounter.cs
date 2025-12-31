using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyEncounter : MonoBehaviour
{
    private bool playerInRange = false;

    [Header("Exactly 3 for now")]
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
            // player team comes from the player, not from the enemy object
            BattleData.playerTeam = PlayerParty.Instance.team;
            BattleData.enemyTeam  = enemyTeam;

            Debug.Log("=== Setting enemy team ===");
            for (int i = 0; i < enemyTeam.Length; i++)
            {
                Debug.Log(
                    enemyTeam[i] != null
                    ? enemyTeam[i].pokemonName
                    : "NULL"
                );
            }


            SceneManager.LoadScene("BattleScene");
        }
    }
}
