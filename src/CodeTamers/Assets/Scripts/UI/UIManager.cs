using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject inventoryWindow;
    public GameObject pokedexWindow;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
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
