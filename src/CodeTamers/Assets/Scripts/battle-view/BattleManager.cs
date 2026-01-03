using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("UI")]
    public BattleEditorController editorUI;
    public BattleUI battleUI;

    [Header("Prefabs")]
    public GameObject playerCreaturePrefab;
    public GameObject enemyCreaturePrefab;

    [Header("Spawn Points")]
    public Transform[] playerSpawnPoints;
    public Transform[] enemySpawnPoints;

    [Header("Battle Logic")]
    public BattleInstructionInterpreter battleInterpreter;
    public BattleActionExecutor actionExecutor;

    private SimpleParser parser = new SimpleParser();

    private List<Creature> playerCreatures = new();
    private List<Creature> enemyCreatures = new();

    // =========================
    // UNITY LIFECYCLE
    // =========================
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

    // =========================
    // SPAWNING
    // =========================
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
                // 1. Parse code
                var instructions = parser.Parse(creature.codeBuffer);

                // 2. Interpret → BattleActions
                var actions = battleInterpreter.Execute(creature, instructions);

                // Syntax error or interpreter stop
                if (actions == null)
                    continue;

                // 3. Execute actions
                foreach (var action in actions)
                {
                    bool ok = actionExecutor.Execute(creature, action);
                    if (!ok)
                        break; // brak many / błąd runtime → stop programu
                }

                // 4. Update UI
                battleUI.UpdateSinglePlayer(i, creature);
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
    // COMMANDS EXECUTED BY ACTION EXECUTOR
    // =========================
    public bool PlayerAttack(Creature attacker, int targetIndex)
    {
        if (targetIndex < 0 || targetIndex >= enemyCreatures.Count)
        {
            Debug.Log($"attack({targetIndex}) – invalid target");
            return false;
        }

        Creature target = enemyCreatures[targetIndex];

        if (target.currentHP <= 0)
        {
            Debug.Log("Target already defeated");
            return false;
        }

        int damage = Mathf.Max(
            1,
            attacker.data.attack - target.data.defense
        );

        target.currentHP -= damage;

        Debug.Log(
            $"{attacker.data.pokemonName} attacks " +
            $"{target.data.pokemonName} for {damage} dmg"
        );

        battleUI.UpdateSingleEnemy(targetIndex, target);
        return true;
    }

    public void PlayerBlock(Creature blocker)
    {
        Debug.Log($"{blocker.data.pokemonName} BLOCKS");
    }
}
