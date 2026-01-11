using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public bool canMove = true;

    public InputActionReference moveAction;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    [Header("Footstep Settings")]
    public AudioClip walkClip;       // Twój jedyny clip kroków
    public float stepDelay = 0.4f;   // czas między krokami
    private float stepTimer = 0f;    // licznik czasu do następnego kroku



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        moveAction.action.Enable();
    }

    void OnDisable()
    {
        moveAction.action.Disable();
    }

    void Update()
    {
        if (!canMove)
        {
            moveInput = Vector2.zero;
            return;
        }

        moveInput = moveAction.action.ReadValue<Vector2>();

        // ----------------- krok -----------------
        if (moveInput.magnitude > 0.1f)
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepDelay)
            {
                //AudioManager.instance.PlaySFX(walkClip);
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = stepDelay; // reset timera jeśli gracz stoi
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
    }

    
}
