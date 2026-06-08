using System.Collections;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private float flySpeed = 8f;
    [SerializeField] private float pickupDistance = 0.15f;

    private ItemData itemData;
    private int amount;

    private Transform targetPlayer;
    private bool flyingToPlayer;

    public void SetItem(ItemData data, int itemAmount)
    {
        itemData = data;
        amount = itemAmount;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (flyingToPlayer)
            return;

        if (other.CompareTag("Player"))
        {
            targetPlayer = other.transform;
            flyingToPlayer = true;
            StartCoroutine(FlyToPlayer());
        }
    }

    private IEnumerator FlyToPlayer()
    {
        Collider2D col = GetComponent<Collider2D>();

        if (col != null)
            col.enabled = false;

        while (Vector2.Distance(transform.position, targetPlayer.position) > pickupDistance)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPlayer.position,
                flySpeed * Time.deltaTime
            );

            yield return null;
        }

        if (PlayerInventory.Instance != null && itemData != null)
        {
            PlayerInventory.Instance.AddItem(itemData, amount);
        }

        Destroy(gameObject);
    }
}