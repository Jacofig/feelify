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
    private bool initialized = false;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (!initialized)
        {
            Initialize();
            initialized = true;
        }
    }

    void Initialize()
    {
        Debug.Log("INITIALIZE INVENTORY CALLED\n" + System.Environment.StackTrace);

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
        Debug.Log("ADD ITEM CALLED FROM:\n" + new System.Diagnostics.StackTrace(true));
        Debug.Log("PlayerInventory instance: " + GetInstanceID());
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

    public void RemoveItem(string itemName, int amount)
    {
        for (int i = items.Count - 1; i >= 0; i--)
        {
            var item = items[i];
            Debug.Log($"Checking item: {item.data.itemName}");
            if (item.data != null &&
                (string.Equals(item.data.itemName, itemName, StringComparison.OrdinalIgnoreCase) ||
                 string.Equals(item.data.name, itemName, StringComparison.OrdinalIgnoreCase)))
            {
                int toRemove = Mathf.Min(item.amount, amount);

                item.amount -= toRemove;
                amount -= toRemove;

                if (item.amount <= 0)
                {
                    items.RemoveAt(i);
                }

                if (amount <= 0)
                    return;
            }
        }
    }
}
