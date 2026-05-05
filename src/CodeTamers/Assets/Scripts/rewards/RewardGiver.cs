using UnityEngine;
using System.Collections.Generic;

public class RewardGiver : MonoBehaviour
{
    [System.Serializable]
    public class Reward
    {
        public ItemData item;
        public int amount = 1;
    }

    public List<Reward> rewards = new();

    public void GiveRewards()
    {
        foreach (var r in rewards)
        {
            if (r.item == null)
                continue;

            PlayerInventory.Instance.AddItem(r.item, r.amount);

            Debug.Log($"Gave: {r.item.name} x{r.amount}");
        }
    }
}