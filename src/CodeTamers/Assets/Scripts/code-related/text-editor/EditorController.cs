using UnityEngine;
using TMPro;
using System;

public class EditorController : MonoBehaviour
{
    public TMP_InputField inputField;
    public PlayerMovement playerMovement;

    public InstructionInterpreter interpreter;

    private bool isOpen = true;

    // 🔴 TO DODAJ
    void OnEnable()
    {
        if (interpreter == null)
        {
            interpreter = FindObjectOfType<InstructionInterpreter>();

            if (interpreter != null)
                Debug.Log("Interpreter auto-found: " + interpreter.name);
            else
                Debug.LogError("Interpreter NOT found in scene!");
        }
    }

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

        inputField.text = "";
    }

    public void SubmitCode()
    {
        if (interpreter == null)
        {
            Debug.LogError("Interpreter is NULL w EditorController!");
            return;
        }

        string code = inputField.text;
        SimpleParser parser = new SimpleParser();

        try
        {
            var instructions = parser.Parse(code);
            interpreter.Execute(instructions);
            Debug.Log("Kod wykonany");
        }
        catch (Exception ex)
        {
            Debug.LogError("Błąd parsowania: " + ex.Message);
        }
    }

    public bool IsOpen()
    {
        return isOpen;
    }

    void Start()
    {
        gameObject.SetActive(true);
    }
}
