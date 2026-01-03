using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Visual Scaling")]
    public float playerScale = 4f;
    public float enemyScale = 4f;

    [Header("UI")]
    public BattleEditorController editorUI;
    public BattleUI battleUI;

    [Header("Prefabs")]
    public GameObject playerCreaturePrefab;
    public GameObject enemyCreaturePrefab;

    [Header("Spawn Points")]
    public RectTransform[] playerUISlots;
    public RectTransform[] enemyUISlots;


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
        SpawnTeam(BattleData.playerTeam, playerCreaturePrefab, playerUISlots, playerCreatures, playerScale);
        SpawnTeam(BattleData.enemyTeam, enemyCreaturePrefab, enemyUISlots, enemyCreatures, playerScale);


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

    Vector3 UIToWorldPosition(RectTransform uiSlot)
    {
        Camera cam = Camera.main;

        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(cam, uiSlot.position);

        Vector3 worldPos = cam.ScreenToWorldPoint(
            new Vector3(screenPos.x, screenPos.y, cam.nearClipPlane + 1f)
        );

        worldPos.z = 0f; // critical for 2D

        return worldPos;
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
        for (int i = 0; i < playerCreatures.Count; i++)
        {
            battleUI.UpdateSinglePlayer(i, playerCreatures[i]);
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
        RectTransform[] uiSlots,
        List<Creature> outList,
        float visualScale
    )
    {
        outList.Clear();

        for (int i = 0; i < teamData.Length; i++)
        {
            if (teamData[i] == null)
                continue;

            Vector3 worldPos = UIToWorldPosition(uiSlots[i]);

            var obj = Instantiate(prefab, worldPos, Quaternion.identity);
            obj.transform.localScale = Vector3.one * visualScale;
            var sr = obj.GetComponentInChildren<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = -1;
            }
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
