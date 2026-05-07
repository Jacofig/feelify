using UnityEngine;
using System.Collections;

public class EnemySpawnerINVIDUAL : MonoBehaviour
{
    [System.Serializable]
    public class SpawnData
    {
        public GameObject prefab;
        public Transform spawnPoint;
        public float respawnTime = 15f;

        [HideInInspector] public GameObject currentInstance;
        [HideInInspector] public bool isRespawning = false;
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public SpawnData enemy;

    void Start()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        enemy.currentInstance = Instantiate(
            enemy.prefab,
            enemy.spawnPoint.position,
            enemy.spawnPoint.rotation
        );

        SpawnedEnemy se = enemy.currentInstance.GetComponent<SpawnedEnemy>();

        if (se != null)
        {
            se.spawner = this;
        }

        enemy.isRespawning = false;
    }

    public void OnEnemyKilled()
    {
        if (!enemy.isRespawning)
        {
            StartCoroutine(RespawnRoutine());
        }
    }

    IEnumerator RespawnRoutine()
    {
        enemy.isRespawning = true;

        yield return new WaitForSeconds(enemy.respawnTime);

        SpawnEnemy();
    }
}