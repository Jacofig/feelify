using UnityEngine;
using System.Collections.Generic;
using System;
public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    [Header("Starting Items (Inspector)")]
    public List<ItemStack> startingItems = new();

    [Header("Runtime Inventory")]
    public List<ItemStack> items = new();

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Initialize();
    }

    void Initialize()
    {
        items.Clear();

        foreach (var stack in startingItems)
        {
            if (stack.data == null || stack.amount <= 0)
                continue;

            items.Add(new ItemStack
            {
                data = stack.data,
                amount = stack.amount
            });
        }
    }

    public void AddItem(ItemData data, int amount = 1)
    {
        var existing = items.Find(i => i.data == data && data.stackable);

        if (existing != null)
            existing.amount += amount;
        else
            items.Add(new ItemStack { data = data, amount = amount });
    }

    public int GetItemCount(string itemId)
    {
        int count = 0;

        foreach (var item in items)
        {
            if (item.data == null)
                continue;

            if (string.Equals(item.data.itemName, itemId, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(item.data.name, itemId, StringComparison.OrdinalIgnoreCase))
            {
                count += item.amount;
            }
        }

        return count;
    }
}
