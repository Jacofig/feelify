using UnityEngine;
public class UIManager : MonoBehaviour
{
    public GameObject inventoryWindow;
    public GameObject pokedexWindow;
    public ItemData Metal;
    public ItemData Coal;

    public ItemData Stick;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
            PlayerInventory.Instance.AddItem(Metal, 5);
            PlayerInventory.Instance.AddItem(Coal, 2);
            PlayerInventory.Instance.AddItem(Stick, 1);

        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePokedex();
        }
    }

    public void ToggleInventory()
    {
        Toggle(inventoryWindow);
    }

    public void TogglePokedex()
    {
        Toggle(pokedexWindow);
    }

    void Toggle(GameObject window)
    {
        bool open = window.activeSelf;
        window.SetActive(!open);

        if (!open)
            window.transform.SetAsLastSibling();
    }
}
