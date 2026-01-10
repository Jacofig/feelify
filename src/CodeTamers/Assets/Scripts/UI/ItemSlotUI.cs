using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public Image icon;
    public TMP_Text amountText;

    ItemStack stack;

    public void Setup(ItemStack stack)
    {
        this.stack = stack;
        icon.sprite = stack.data.icon;
        amountText.text = stack.amount > 1 ? stack.amount.ToString() : "";
    }
}
