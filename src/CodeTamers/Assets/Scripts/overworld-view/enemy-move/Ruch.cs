using UnityEngine;
using System.Collections.Generic;

public class Ruch : MonoBehaviour
{
    [Header("Punkty patrolu")]
    public List<Transform> punktyPatrolu;
    public float predkosc = 2f;
    private int aktualnyPunkt = 0;

    [Header("Chase Player")]
    public Transform gracz;
    public float zasięgWidzenia = 5f;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 kierunek = Vector2.zero;

        if (gracz != null && Vector2.Distance(transform.position, gracz.position) <= zasięgWidzenia)
        {
            // Goni gracza
            kierunek = (gracz.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, gracz.position, predkosc * Time.deltaTime);
        }
        else
        {
            // Patrolowanie punktów
            if (punktyPatrolu.Count == 0)
                return;

            Transform cel = punktyPatrolu[aktualnyPunkt];
            kierunek = (cel.position - transform.position).normalized;

            transform.position = Vector2.MoveTowards(transform.position, cel.position, predkosc * Time.deltaTime);

            if (Vector2.Distance(transform.position, cel.position) < 0.1f)
            {
                aktualnyPunkt++;
                if (aktualnyPunkt >= punktyPatrolu.Count)
                {
                    aktualnyPunkt = 0;
                }
            }
        }

        // Animacja chodzenia w dół
        if (kierunek.y < 0f)  // jeśli idzie w dół
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
