using UnityEngine;
using System.Diagnostics;
using System.IO;
using UnityEngine.InputSystem;

public class StatueInteractor : MonoBehaviour
{
    public GameObject interactText;
    public EditorController editorController;

    private bool playerNear = false;

    void Update()
    {
        if (playerNear && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (!editorController.IsOpen())
            {
                editorController.OpenEditor();
                interactText.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerNear = true;
            interactText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerNear = false;
            interactText.SetActive(false);
        }
    }
}
