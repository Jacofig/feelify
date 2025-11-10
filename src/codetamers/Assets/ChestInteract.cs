using UnityEngine;
using UnityEngine.InputSystem;

public class ChestInteract : MonoBehaviour
{
    public GameObject openChestPrefab;
    public GameObject interactText;

    private bool playerNear = false;

    void Update()
    {
        if (playerNear && Keyboard.current.eKey.wasPressedThisFrame)
        {
            OpenChest();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNear = true;
            interactText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNear = false;
            interactText.SetActive(false);
        }
    }

    void OpenChest()
    {
        Instantiate(openChestPrefab, transform.position, transform.rotation);
        interactText.SetActive(false);
        Destroy(gameObject);
    }
}
