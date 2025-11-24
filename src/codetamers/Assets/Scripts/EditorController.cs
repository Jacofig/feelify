using UnityEngine;
using TMPro;
using System.IO;

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
        Debug.Log("Submituje Kod");
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
