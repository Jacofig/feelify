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

    private Animator anim;





    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void OnEnable()
    {
        moveAction.action.Enable();
        anim = GetComponent<Animator>();
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



        // ---------- ANIMACJE RUCHU ----------

        // idle
        if (moveInput.magnitude < 0.1f)
        {
            anim.SetBool("walk_f", false);
            anim.SetBool("walk_b", false);
            anim.SetBool("walk_l", false);
            anim.SetBool("walk_r", false);
            return;
        }

        // wybór osi dominującej
        if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
        {
            if (moveInput.x > 0)
            {
                anim.SetBool("walk_r", true);
                anim.SetBool("walk_l", false);
            }
            else
            {
                anim.SetBool("walk_l", true);
                anim.SetBool("walk_r", false);
            }

            anim.SetBool("walk_f", false);
            anim.SetBool("walk_b", false);
        }
        else
        {
            if (moveInput.y > 0)
            {
                anim.SetBool("walk_b", true);
                anim.SetBool("walk_f", false);
            }
            else
            {
                anim.SetBool("walk_f", true);
                anim.SetBool("walk_b", false);
            }

            anim.SetBool("walk_l", false);
            anim.SetBool("walk_r", false);
        }




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
