using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Transform content;
    public GameObject itemSlotPrefab;

    void OnEnable()
    {
        Refresh();
    }

    void Refresh()
    {
        foreach (Transform c in content)
            Destroy(c.gameObject);

        foreach (var stack in PlayerInventory.Instance.items)
        {
            GameObject slot = Instantiate(itemSlotPrefab, content);
            slot.GetComponent<ItemSlotUI>().Setup(stack);
        }
    }
}
