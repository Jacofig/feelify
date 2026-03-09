using UnityEngine;

public class AttackSpawner : MonoBehaviour
{
    public GameObject attackPrefab;
    public float spawnRate = 0.1f;

    float timer;

    void Update()
    {
        if (GameManager.instance.gameEnded) return;

        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnAttack();
            timer = 0;
        }
    }

    void SpawnAttack()
    {
        int side = Random.Range(0, 4);

        Vector3 pos = Vector3.zero;
        Vector2 dir = Vector2.zero;

        if (side == 0) // LEFT
        {
            pos = new Vector3(-4.5f, Random.Range(-2f, 2f), 0);
            dir = Vector2.right;
        }
        else if (side == 1) // RIGHT
        {
            pos = new Vector3(4.5f, Random.Range(-2f, 2f), 0);
            dir = Vector2.left;
        }
        else if (side == 2) // TOP
        {
            pos = new Vector3(Random.Range(-4f, 4f), 2.5f, 0);
            dir = Vector2.down;
        }
        else // BOTTOM
        {
            pos = new Vector3(Random.Range(-4f, 4f), -2.5f, 0);
            dir = Vector2.up;
        }

        GameObject atk = Instantiate(attackPrefab, pos, Quaternion.identity);

        atk.GetComponent<Attack>().direction = dir;
    }
}