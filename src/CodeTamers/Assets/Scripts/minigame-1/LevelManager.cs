using UnityEngine;
using TMPro; // jeúli uøywasz TextMeshPro

public class LevelManager : MonoBehaviour
{
    public GameObject[] levels; // prefaby map
    public GameObject ballPrefab;

    private GameObject currentLevel;
    private Rigidbody2D ball;

    [Header("UI")]
    public TextMeshProUGUI collectibleText;
    public TextMeshProUGUI movesText;

    private int currentLevelIndex = 0;
    private int totalCollectibles;
    private int collected = 0;

    private int moveCount = 0;
    public int maxMoves = 20;

    void Start()
    {
        LoadLevel(currentLevelIndex);
    }

    void LoadLevel(int index)
    {
        if (currentLevel != null)
            Destroy(currentLevel);

        currentLevel = Instantiate(levels[index]);

        // jeúli kulka jest prefabem w Level, znajdü jπ
        ball = currentLevel.GetComponentInChildren<Rigidbody2D>();

        // przypisz LevelRotate do kulki
        LevelRotate rotateScript = currentLevel.GetComponent<LevelRotate>();
        if (rotateScript != null && ball != null)
            rotateScript.SetBall(ball);

        // zlicz collectibles
        Collectible[] cs = currentLevel.GetComponentsInChildren<Collectible>();
        totalCollectibles = cs.Length;
        collected = 0;

        // reset ruchÛw
        moveCount = 0;

        UpdateUI();
    }

    public void Collect()
    {
        collected++;
        UpdateUI();

        if (collected >= totalCollectibles)
        {
            Debug.Log("LEVEL COMPLETED");
            NextLevel();
        }
    }

    // wywo≥aj przy kaødym obrocie planszy
    public void OnMove()
    {
        moveCount++;
        UpdateUI();

        if (moveCount >= maxMoves)
        {
            Debug.Log("RESTART LEVEL!");
            LoadLevel(currentLevelIndex);
        }
    }

    void UpdateUI()
    {
        if (collectibleText != null)
            collectibleText.text = $"{collected}/{totalCollectibles}";

        if (movesText != null)
            movesText.text = $"{moveCount}/{maxMoves}";
    }

    public void NextLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex >= levels.Length)
        {
            Debug.Log("ALL LEVELS COMPLETED!");
            currentLevelIndex = 0;
        }
        LoadLevel(currentLevelIndex);
    }
}