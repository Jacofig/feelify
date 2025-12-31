using System;
using UnityEngine;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    public BattleEditorController editorUI;
    public BattleUI battleUI;
    public GameObject playerCreaturePrefab;
    public GameObject enemyCreaturePrefab;

    public Transform[] playerSpawnPoints; // size 3
    public Transform[] enemySpawnPoints;  // size 3

    private SimpleParser parser = new SimpleParser();

    private List<Creature> playerCreatures = new();
    private List<Creature> enemyCreatures = new();


    // You'll have some battle interpreter that can execute in battle context
    public BattleInstructionInterpreter battleInterpreter;

    void Start()
    {
        Debug.Log("=== BattleManager received enemy team ===");

        if (BattleData.enemyTeam == null)
        {
            Debug.LogError("BattleData.enemyTeam IS NULL");
        }
        else
        {
            for (int i = 0; i < BattleData.enemyTeam.Length; i++)
            {
                Debug.Log(
                    BattleData.enemyTeam[i] != null
                    ? BattleData.enemyTeam[i].pokemonName
                    : "NULL"
                );
            }
        }

        editorUI.Init(this);

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

        // TEMP: show first Pokémon in UI
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
            {
                Debug.LogError($"Team slot {i} is NULL!");
                continue;
            }

            var obj = Instantiate(prefab, spawns[i].position, Quaternion.identity);
            var creature = obj.GetComponent<Creature>();

            creature.Init(teamData[i]);
            outList.Add(creature);

            Debug.Log($"Spawned {teamData[i].pokemonName}");
        }
    }

    public void OpenEditorForCreature(Creature c)
    {
        editorUI.BindCreature(c);
        editorUI.OpenEditor();
    }
    public void PlayerAttack()
    {
        Debug.Log("PlayerAttack called (stub)");

        // TEMP example: do nothing or basic logic
    }

    public void PlayerBlock()
    {
        Debug.Log("PlayerBlock called (stub)");
    }


    public void ExecuteCreatureCode(Creature creature, string code)
    {
        var instructions = parser.Parse(code);

        // Example mana rule: 1 mana per instruction
        int manaCost = instructions.Count;

        if (creature.currentMana < manaCost)
        {
            Debug.Log("Not enough mana!");
            return;
        }

        creature.currentMana -= manaCost;
        battleUI.UpdateSinglePlayer(/*index*/ 0, creature); // or find index properly

        // Execute with context = this creature
        battleInterpreter.Execute(creature, instructions);

        Debug.Log($"Executed {instructions.Count} instructions for {creature.data.pokemonName}");
    }
}
