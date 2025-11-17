using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Diagnostics;

public class npc : MonoBehaviour
{
    public GameObject interactText;      
    public TMP_InputField inputField;
    public PlayerMovement playerMovement;
    private bool playerNear = false;

    void Update()
    {
        if (playerNear && Keyboard.current.eKey.wasPressedThisFrame)
        {
            OpenInputField();
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
            inputField.gameObject.SetActive(false);
        }
    }

    void OpenInputField()
    {
        inputField.gameObject.SetActive(true); 
        inputField.ActivateInputField();         
        interactText.SetActive(false);

        if (playerMovement != null)
            playerMovement.canMove = false;
    }

    public void SubmitText()
    {
        string playerInput = inputField.text;


        inputField.text = "";
        inputField.gameObject.SetActive(false);

        if (playerMovement != null)
            playerMovement.canMove = true;
    }
}
