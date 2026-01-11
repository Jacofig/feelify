using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class BattleManager : MonoBehaviour
{
    [Header("Visual Scaling")]
    public float playerScale = 4f;
    public float enemyScale = 4f;
    [Header("Battle End UI")]
    public GameObject battleEndPanel;

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
        RefreshAllUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ExecuteFullRound();
        }
    }

    void RefreshAllUI()
    {
        for (int i = 0; i < playerCreatures.Count; i++)
            battleUI.UpdateSinglePlayer(i, playerCreatures[i]);

        for (int i = 0; i < enemyCreatures.Count; i++)
            battleUI.UpdateSingleEnemy(i, enemyCreatures[i]);
    }

    // =========================
    // FLOW
    // =========================

    public void ExecuteFullRound()
    {
        if (phase != BattlePhase.PlayerTurn)
            return;

        StartCoroutine(RunFullRoundCoroutine());
    }

    private IEnumerator RunFullRoundCoroutine()
    {
        editorUI.SetGoButton(false);

        Debug.Log("=== ROUND START ===");

        // ===== PLAYER TURN =====
        phase = BattlePhase.PlayerTurn;
        yield return StartCoroutine(ExecutePlayerTurnCoroutine());

        RefreshAllUI();
        if (IsBattleOver()) yield break;

        // ===== ENEMY TURN =====
        phase = BattlePhase.EnemyTurn;
        yield return StartCoroutine(ExecuteEnemyTurnCoroutine());

        RefreshAllUI();
        if (IsBattleOver()) yield break;

        LogAllHP();

        phase = BattlePhase.PlayerTurn;
        editorUI.SetGoButton(true);

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
    private IEnumerator ExecutePlayerTurnCoroutine()
    {
        Debug.Log("=== PLAYER TURN ===");

        ResetMana(playerCreatures);
        actionExecutor.ResetTurn();
        editorUI.SaveActiveCode();

        foreach (var c in playerCreatures)
            c.OnTurnStart();

        for (int i = 0; i < playerCreatures.Count; i++)
        {
            var creature = playerCreatures[i];
            if (creature.CurrentHP <= 0)
                continue;

            if (string.IsNullOrWhiteSpace(creature.codeBuffer))
                continue;

            var instructions = parser.Parse(creature.codeBuffer);
            var actions = battleInterpreter.Execute(creature, instructions);
            if (actions == null)
                continue;

            foreach (var action in actions)
            {
                bool didAction = false;

                yield return StartCoroutine(
                    actionExecutor.ExecuteAction(
                        creature,
                        enemyCreatures,
                        action,
                        result => didAction = result
                    )
                );

                if (!didAction)
                    continue;
            }

        }
    }


    // =========================
    // ENEMY TURN
    // =========================
    private IEnumerator ExecuteEnemyTurnCoroutine()
    {
        Debug.Log("=== ENEMY TURN ===");

        ResetMana(enemyCreatures);

        foreach (var c in enemyCreatures)
            c.OnTurnStart();

        for (int i = 0; i < enemyCreatures.Count; i++)
        {
            var enemy = enemyCreatures[i];
            if (enemy.CurrentHP <= 0)
                continue;

            var actions = enemyStrategy.GetActions(enemy, playerCreatures);

            foreach (var action in actions)
            {
                bool didAction = false;

                yield return StartCoroutine(
                    actionExecutor.ExecuteAction(
                        enemy,
                        playerCreatures,
                        action,
                        result => didAction = result
                    )
                );

                if (!didAction)
                    continue;
            }

        }
    }

    void LateUpdate()
    {
        // keep player creatures aligned to UI
        for (int i = 0; i < playerCreatures.Count; i++)
        {
            if (i >= playerUISlots.Length) continue;

            Vector3 pos = UIToWorldPosition(playerUISlots[i]);
            playerCreatures[i].transform.position = pos;
        }

        // keep enemy creatures aligned to UI
        for (int i = 0; i < enemyCreatures.Count; i++)
        {
            if (i >= enemyUISlots.Length) continue;

            Vector3 pos = UIToWorldPosition(enemyUISlots[i]);
            enemyCreatures[i].transform.position = pos;
        }
    }

    // =========================
    // UTIL
    // =========================
    void ResetMana(List<Creature> creatures)
    {
        foreach (var c in creatures)
            c.ResetMana();
    }

    void ShowBattleEndUI(bool playerWon)
    {
        if (battleEndPanel != null)
            battleEndPanel.SetActive(true);

        // Optional: pass result to UI
        battleEndPanel
            .GetComponent<BattleEndUI>()
            ?.SetResult(playerWon);
    }

    bool IsBattleOver()
    {
        bool playerAlive = playerCreatures.Exists(c => c.CurrentHP > 0);
        bool enemyAlive = enemyCreatures.Exists(c => c.CurrentHP > 0);

        if (!playerAlive || !enemyAlive)
        {
            if (phase != BattlePhase.BattleEnd)
            {
                phase = BattlePhase.BattleEnd;
                Debug.Log(playerAlive ? "PLAYER WINS" : "ENEMY WINS");

                ShowBattleEndUI(playerAlive);
            }

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
        if (target.CurrentHP <= 0)
            return false;

        int rawDmg = attacker.data.attack;
        int dealt = target.TakeDamage(rawDmg);



        Debug.Log($"{attacker.data.pokemonName} attacks {target.data.pokemonName} for {dealt}");
        return true;


    }


    public void PlayerBlock(Creature caster, int targetIndex = -1)
    {
        List<Creature> team =
            playerCreatures.Contains(caster)
                ? playerCreatures
                : enemyCreatures;

        Creature target;

        // self-cast
        if (targetIndex < 0)
        {
            target = caster;
        }
        else
        {
            if (targetIndex < 0 || targetIndex >= team.Count)
                return;

            target = team[targetIndex];
        }

        int armorValue = caster.data.defense;

        target.AddStatus(new BlockArmorEffect(armorValue));

        Debug.Log($"{caster.data.pokemonName} gives {armorValue} block to {target.data.pokemonName}");
    }

    public bool PlayerCatch(
     Creature catcher,
     List<Creature> targets,
     int targetIndex
 )
    {
        if (targetIndex < 0 || targetIndex >= targets.Count)
            return false;

        Creature target = targets[targetIndex];

        // nie ³apiemy martwych
        if (target.CurrentHP <= 0)
            return false;

        int maxHP = target.instance.MaxHP;
        int currentHP = target.CurrentHP;

        float hpRatio = (float)currentHP / maxHP;
        float catchChance = Mathf.Clamp01(1f - hpRatio);


        float roll = Random.value;

        Debug.Log(
            $"Trying to catch {target.data.pokemonName}: " +
            $"{Mathf.RoundToInt(catchChance * 100)}% chance (roll={roll:0.00})"
        );

        if (roll <= catchChance)
        {
            Debug.Log($"CAUGHT {target.data.pokemonName}!");

            PlayerParty.Instance.AddPokemon(target.data);
            // wy³¹czenie z walki
            target.TakeDamage(currentHP);

            return true;
        }

        Debug.Log($"{target.data.pokemonName} escaped!");
        return true; // akcja siê wykona³a, nawet jeœli nieudana
    }



    // =========================
    // SPAWN
    // =========================
    void SpawnTeam(
        PokemonInstance[] team,
        GameObject prefab,
        RectTransform[] uiSlots,
        List<Creature> outList,
        float visualScale
    )
    {
        outList.Clear();

        for (int i = 0; i < team.Length; i++)
        {
            if (team[i] == null)
                continue;

            Vector3 worldPos = UIToWorldPosition(uiSlots[i]);

            var obj = Instantiate(prefab, worldPos, Quaternion.identity);
            obj.transform.localScale = Vector3.one * visualScale;

            var sr = obj.GetComponentInChildren<SpriteRenderer>();
            if (sr != null)
                sr.sortingOrder = -1;

            var creature = obj.GetComponent<Creature>();
            creature.Init(team[i]);   // PokemonInstance
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
            Debug.Log($"P{i}: {c.data.pokemonName} HP={c.CurrentHP}");
        }

        Debug.Log("---- ENEMY TEAM ----");
        for (int i = 0; i < enemyCreatures.Count; i++)
        {
            var c = enemyCreatures[i];
            Debug.Log($"E{i}: {c.data.pokemonName} HP={c.CurrentHP}");
        }
    }

}
