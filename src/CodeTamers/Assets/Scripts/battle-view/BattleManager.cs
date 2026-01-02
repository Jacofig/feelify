using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public BattleEditorController editorUI;
    public BattleUI battleUI;

    public GameObject playerCreaturePrefab;
    public GameObject enemyCreaturePrefab;

    public Transform[] playerSpawnPoints;
    public Transform[] enemySpawnPoints;

    private SimpleParser parser = new SimpleParser();

    private List<Creature> playerCreatures = new();
    private List<Creature> enemyCreatures = new();

    public BattleInstructionInterpreter battleInterpreter;

    void Start()
    {
        SpawnTeam(
            BattleData.playerTeam,
            playerCreaturePrefab,
            playerSpawnPoints,
            playerCreatures
        );

        SpawnTeam(
            BattleData.enemyTeam,
            enemyCreaturePrefab,
            enemySpawnPoints,
            enemyCreatures
        );

        editorUI.Init(playerCreatures);
        battleUI.SetPlayerTeam(playerCreatures);
        battleUI.SetEnemyTeam(enemyCreatures);
    }

    void SpawnTeam(
        PokemonData[] teamData,
        GameObject prefab,
        Transform[] spawns,
        List<Creature> outList
    )
    {
        outList.Clear();

        for (int i = 0; i < teamData.Length; i++)
        {
            if (teamData[i] == null)
                continue;

            var obj = Instantiate(prefab, spawns[i].position, Quaternion.identity);
            var creature = obj.GetComponent<Creature>();

            creature.Init(teamData[i]);
            outList.Add(creature);
        }
    }

    // =========================
    // PLAYER TURN
    // =========================
    public void ExecutePlayerTurn()
    {
        Debug.Log("=== PLAYER TURN START ===");
        editorUI.SaveActiveCode();

        for (int i = 0; i < playerCreatures.Count; i++)
        {
            var creature = playerCreatures[i];

            if (creature.currentHP <= 0)
                continue;

            if (string.IsNullOrWhiteSpace(creature.codeBuffer))
            {
                Debug.Log($"{creature.data.pokemonName} has no code. Skipping.");
                continue;
            }

            try
            {
                var instructions = parser.Parse(creature.codeBuffer);

                bool ok = battleInterpreter.Execute(creature, instructions);
                battleUI.UpdateSinglePlayer(i, creature);

                if (!ok)
                {
                    Debug.Log("Program stopped (no mana / error)");
                    continue;
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
                return;
            }
        }

        Debug.Log("=== PLAYER TURN END ===");
    }


    // =========================
    // COMMANDS CALLED BY HANDLER
    // =========================
    public bool PlayerAttack(Creature attacker, int targetIndex)
    {
        if (targetIndex < 0 || targetIndex >= enemyCreatures.Count)
        {
            Debug.Log($"attack({targetIndex}) – nieprawidłowy cel");
            return false;
        }

        Creature target = enemyCreatures[targetIndex];

        if (target.currentHP <= 0)
        {
            Debug.Log("Cel już pokonany");
            return false;
        }

        int damage = Mathf.Max(
            1,
            attacker.data.attack - target.data.defense
        );

        target.currentHP -= damage;

        Debug.Log(
            $"{attacker.data.pokemonName} atakuje " +
            $"{target.data.pokemonName} za {damage} dmg"
        );

        battleUI.UpdateSingleEnemy(targetIndex, target);
        return true;
    }


    public void PlayerBlock(Creature blocker)
    {
        Debug.Log($"{blocker.data.pokemonName} BLOCKS");
    }
}
