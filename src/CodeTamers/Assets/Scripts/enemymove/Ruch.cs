using UnityEngine;
using System.Collections.Generic;

public class Ruch : MonoBehaviour
{
    [Header("Punkty patrolu")]
    public List<Transform> punktyPatrolu;
    public float predkosc = 2f;
    private int aktualnyPunkt = 0;

    [Header("Chase Player")]
    public Transform gracz;          // Przeciągnij tutaj obiekt gracza
    public float zasięgWidzenia = 5f; // Jak daleko przeciwnik widzi gracza

    void Update()
    {
        if (gracz != null && Vector2.Distance(transform.position, gracz.position) <= zasięgWidzenia)
        {
            // Goni gracza
            transform.position = Vector2.MoveTowards(transform.position, gracz.position, predkosc * Time.deltaTime);
        }
        else
        {
            // Patrolowanie punktów
            if (punktyPatrolu.Count == 0)
                return;

            Transform cel = punktyPatrolu[aktualnyPunkt];
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
    }
}
