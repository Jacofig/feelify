using UnityEngine;

public class kierunek : MonoBehaviour
{
    private Animator anim;
    private patrol patrolScript;
    private chase chaseScript;
    private flee fleeScript;

    void Start()
    {
        anim = GetComponent<Animator>();
        patrolScript = GetComponent<patrol>();
        chaseScript = GetComponent<chase>();
        fleeScript = GetComponent<flee>();
    }

    void Update()
    {
        Vector2 dir = Vector2.zero;

        // --- jeśli ten NPC ma flee ---
        if (fleeScript != null)
        {
            float dist = Vector2.Distance(transform.position, fleeScript.player.position);
            if (dist < fleeScript.fleeRange)
                dir = fleeScript.GetDirection();
        }

        // --- jeśli ten NPC ma chase ---
        if (dir == Vector2.zero && chaseScript != null)
        {
            float dist = Vector2.Distance(transform.position, chaseScript.player.position);
            if (dist < chaseScript.chaseRange)
                dir = chaseScript.GetDirection();
        }

        // --- jeśli ma patrol ---
        if (dir == Vector2.zero && patrolScript != null && patrolScript.canMove)
        {
            dir = patrolScript.GetDirection();
        }


        // DEADZONE — brak ruchu = idle
        if (dir.magnitude < 0.1f)
        {
            anim.SetBool("walk_f", false);
            anim.SetBool("walk_b", false);
            anim.SetBool("walk_l", false);
            anim.SetBool("walk_r", false);
            return;
        }

        // Wybór dominującej osi — horyzont / pion
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0)
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
            if (dir.y > 0)
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
    }
}
