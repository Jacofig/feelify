using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyEncounter : MonoBehaviour
{
    private bool playerInRange = false;
    public PokemonData enemyPokemonData;
    public PokemonData playerPokemonData;

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
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            BattleData.enemyData = enemyPokemonData;
            BattleData.playerData = playerPokemonData;
            SceneManager.LoadScene("BattleScene");
        }
    }
}
