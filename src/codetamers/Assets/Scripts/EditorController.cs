using UnityEngine;
using TMPro;
using System.IO;
using System;
public class EditorController : MonoBehaviour
{
    public TMP_InputField inputField;
    public PlayerMovement playerMovement;

    private bool isOpen = false;

    public void OpenEditor()
    {
        gameObject.SetActive(true);
        inputField.ActivateInputField();
        isOpen = true;

        if (playerMovement != null)
            playerMovement.canMove = false;

    }

    public void CloseEditor()
    {
        gameObject.SetActive(false);
        isOpen = false;


        if (playerMovement != null)
            playerMovement.canMove = true;
    }

    public void SubmitCode()
    {
        string code = inputField.text;

        SimpleParser parser = new SimpleParser();

        try
        {
            var instructions = parser.Parse(code);

            Debug.Log("Kod sparsowany poprawnie:");
            foreach (var instr in instructions)
            {
                Debug.Log($"{instr.Type} - {instr.Name} (linia {instr.LineNumber})");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("B³¹d parsowania: " + ex.Message);
        }
    }

    public bool IsOpen()
    {
        return isOpen;
    }

    void Start()
    {
        gameObject.SetActive(false);
    }
}
