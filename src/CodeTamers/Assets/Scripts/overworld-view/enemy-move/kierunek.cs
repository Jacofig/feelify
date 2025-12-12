using UnityEngine;

public class kierunek : MonoBehaviour
{
    private Animator anim;
    private patrol patrolScript;
    private chase chaseScript;

    void Start()
    {
        anim = GetComponent<Animator>();
        patrolScript = GetComponent<patrol>();
        chaseScript = GetComponent<chase>();
    }

    void Update()
    {
        // Wybieramy aktualny kierunek ruchu: chase ma pierwszeństwo
        Vector2 dir = Vector2.zero;

        if (chaseScript != null && Vector2.Distance(transform.position, chaseScript.player.position) < chaseScript.chaseRange)
        {
            dir = chaseScript.GetDirection();
        }
        else if (patrolScript != null && patrolScript.canMove)
        {
            dir = patrolScript.GetDirection();
        }

        // Deadzone – jeśli ruch jest minimalny, ustawiamy idle
        if (dir.magnitude < 0.1f)
        {
            anim.SetBool("walk_f", false);
            anim.SetBool("walk_b", false);
            anim.SetBool("walk_l", false);
            anim.SetBool("walk_r", false);
            return;
        }

        // Określamy dominującą oś
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            // Ruch poziomy
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
            // Ruch pionowy
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
