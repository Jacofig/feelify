using System.Collections;
using UnityEngine;

public class MineableRock : MonoBehaviour
{
    [Header("Rock HP")]
    [SerializeField] private int maxHp = 3;
    private int currentHp;

    [Header("Sprites")]
    [SerializeField] private Sprite[] hpSprites; 
    // index 0 = full hp, index 1 = damaged, index 2 = almost destroyed

    [Header("Interaction")]
    [SerializeField] private GameObject ePrompt;

    [Header("Drops")]
    [SerializeField] private GameObject pickupPrefab;
    [SerializeField] private ItemData droppedItem;
    [SerializeField] private int dropAmount = 3;
    [SerializeField] private float dropSpread = 0.6f;

    [Header("Hit Effect")]
    [SerializeField] private float shakeDuration = 0.12f;
    [SerializeField] private float shakeStrength = 0.06f;

    private SpriteRenderer spriteRenderer;
    private bool playerInRange;
    private bool destroyed;

    private void Awake()
    {
        currentHp = maxHp;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (ePrompt != null)
            ePrompt.SetActive(false);

        UpdateSprite();
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !destroyed)
        {
            Mine();
        }
    }

    private void Mine()
    {
        currentHp--;

        StopAllCoroutines();
        StartCoroutine(Shake());

        UpdateSprite();

        if (currentHp <= 0)
        {
            DestroyRock();
        }
    }

    private void UpdateSprite()
    {
        if (hpSprites == null || hpSprites.Length == 0)
            return;

        int damageStage = maxHp - currentHp;

        if (damageStage >= 0 && damageStage < hpSprites.Length)
        {
            spriteRenderer.sprite = hpSprites[damageStage];
        }
    }

    private IEnumerator Shake()
    {
        Vector3 originalPos = transform.localPosition;
        float timer = 0f;

        while (timer < shakeDuration)
        {
            float x = Random.Range(-shakeStrength, shakeStrength);
            float y = Random.Range(-shakeStrength, shakeStrength);

            transform.localPosition = originalPos + new Vector3(x, y, 0f);

            timer += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }

    private void DestroyRock()
    {
        destroyed = true;

        if (ePrompt != null)
            ePrompt.SetActive(false);

        for (int i = 0; i < dropAmount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * dropSpread;

            GameObject drop = Instantiate(
                pickupPrefab,
                transform.position + (Vector3)randomOffset,
                Quaternion.identity
            );

            PickupItem pickup = drop.GetComponent<PickupItem>();
            pickup.SetItem(droppedItem, 1);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (ePrompt != null)
                ePrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (ePrompt != null)
                ePrompt.SetActive(false);
        }
    }
}