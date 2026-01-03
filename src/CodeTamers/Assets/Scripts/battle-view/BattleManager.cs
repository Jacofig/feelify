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

    private BattlePhase phase = BattlePhase.PlayerTurn;

    private IEnemyTurnStrategy enemyStrategy = new EnemyMirrorAttackStrategy();

    // =========================
    // UNITY
    // =========================
    void Start()
    {
        SpawnTeam(BattleData.playerTeam, playerCreaturePrefab, playerSpawnPoints, playerCreatures);
        SpawnTeam(BattleData.enemyTeam, enemyCreaturePrefab, enemySpawnPoints, enemyCreatures);

        editorUI.Init(playerCreatures);
        battleUI.SetPlayerTeam(playerCreatures);
        battleUI.SetEnemyTeam(enemyCreatures);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ExecuteFullRound();
        }
    }


    // =========================
    // FLOW
    // =========================
    public void ExecuteFullRound()
    {
        if (phase == BattlePhase.BattleEnd)
            return;

        Debug.Log("=== ROUND START ===");

        phase = BattlePhase.PlayerTurn;
        ExecutePlayerTurn();

        if (IsBattleOver())
            return;

        phase = BattlePhase.EnemyTurn;
        ExecuteEnemyTurn();

        if (IsBattleOver())
            return;

        LogAllHP();

        phase = BattlePhase.PlayerTurn;

        Debug.Log("=== ROUND END ===");
    }

 

    // =========================
    // PLAYER TURN
    // =========================
    void ExecutePlayerTurn()
    {
        Debug.Log("=== PLAYER TURN ===");

        ResetMana(playerCreatures);
        editorUI.SaveActiveCode();

        for (int i = 0; i < playerCreatures.Count; i++)
        {
            var creature = playerCreatures[i];
            if (creature.currentHP <= 0)
                continue;

            if (string.IsNullOrWhiteSpace(creature.codeBuffer))
                continue;

            var instructions = parser.Parse(creature.codeBuffer);
            var actions = battleInterpreter.Execute(creature, instructions);
            if (actions == null)
                continue;

            foreach (var action in actions)
            {
                if (!actionExecutor.Execute(creature, enemyCreatures, action))
                    break;
            }


            battleUI.UpdateSinglePlayer(i, creature);
        }
    }

    // =========================
    // ENEMY TURN
    // =========================
    void ExecuteEnemyTurn()
    {
        Debug.Log("=== ENEMY TURN ===");

        ResetMana(enemyCreatures);

        for (int i = 0; i < enemyCreatures.Count; i++)
        {
            var enemy = enemyCreatures[i];
            if (enemy.currentHP <= 0)
                continue;

            var actions = enemyStrategy.GetActions(enemy, playerCreatures);

            foreach (var action in actions)
            {
                if (!actionExecutor.Execute(enemy, playerCreatures, action))
                    break;
            }


            battleUI.UpdateSingleEnemy(i, enemy);
        }
    }

    // =========================
    // UTIL
    // =========================
    void ResetMana(List<Creature> creatures)
    {
        foreach (var c in creatures)
        {
            c.currentMana = c.data.maxMana;
        }
    }

    bool IsBattleOver()
    {
        bool playerAlive = playerCreatures.Exists(c => c.currentHP > 0);
        bool enemyAlive = enemyCreatures.Exists(c => c.currentHP > 0);

        if (!playerAlive || !enemyAlive)
        {
            phase = BattlePhase.BattleEnd;
            Debug.Log(playerAlive ? "PLAYER WINS" : "ENEMY WINS");
            return true;
        }

        return false;
    }


    // =========================
    // ACTIONS CALLED BY EXECUTOR
    // =========================
    public bool PlayerAttack(
    Creature attacker,
    List<Creature> targets,
    int targetIndex)
    {
        if (targetIndex < 0 || targetIndex >= targets.Count)
            return false;

        var target = targets[targetIndex];
        if (target.currentHP <= 0)
            return false;

        int dmg = Mathf.Max(1, attacker.data.attack - target.data.defense);
        target.currentHP -= dmg;

        Debug.Log($"{attacker.data.pokemonName} attacks {target.data.pokemonName} for {dmg}");
        return true;
    }


    public void PlayerBlock(Creature blocker)
    {
        Debug.Log($"{blocker.data.pokemonName} BLOCKS");
    }

    // =========================
    // SPAWN
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
            creature.teamIndex = i;
            outList.Add(creature);
        }
    }
    void LogAllHP()
    {
        Debug.Log("---- PLAYER TEAM ----");
        for (int i = 0; i < playerCreatures.Count; i++)
        {
            var c = playerCreatures[i];
            Debug.Log($"P{i}: {c.data.pokemonName} HP={c.currentHP}");
        }

        Debug.Log("---- ENEMY TEAM ----");
        for (int i = 0; i < enemyCreatures.Count; i++)
        {
            var c = enemyCreatures[i];
            Debug.Log($"E{i}: {c.data.pokemonName} HP={c.currentHP}");
        }
    }

}
