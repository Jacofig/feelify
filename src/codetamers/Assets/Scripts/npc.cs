using UnityEngine;
using TMPro;
using System.Diagnostics;
using System.IO;
using UnityEngine.InputSystem;

public class npc : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject editorCanvas;
    public GameObject interactText;
    public PlayerMovement playerMovement;

    private bool playerNear = false;
    private bool editorOpen = false;

    void Update()
    {
        // Otwieramy edytor tylko jeśli:
        // - gracz jest blisko
        // - edytor jest zamknięty
        // - wciśnięto E
        if (playerNear && !editorOpen && Keyboard.current.eKey.wasPressedThisFrame)
        {
            OpenEditor();
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

            // jeśli wyjdzie i edytor był otwarty, zamknij go
            if (editorOpen)
                ExitEditor();
        }
    }

    void OpenEditor()
    {
        editorCanvas.SetActive(true);
        inputField.text = ""; // lub zostaw bez czyszczenia, jak chcesz

        inputField.ActivateInputField();
        interactText.SetActive(false);

        editorOpen = true;

        if (playerMovement != null)
            playerMovement.canMove = false;
    }

    public void SubmitText()
    {
        string code = inputField.text;

        // zapis kodu do pliku
        string folder = Application.dataPath + "/Input";

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        string path = folder + "/user_code.txt";
        File.WriteAllText(path, code);

        UnityEngine.Debug.Log("Zapisano kod do: " + path);

        ExitEditor();
    }

    public void ExitEditor()
    {
        editorCanvas.SetActive(false);
        editorOpen = false;

        if (playerMovement != null)
            playerMovement.canMove = true;
    }
}
