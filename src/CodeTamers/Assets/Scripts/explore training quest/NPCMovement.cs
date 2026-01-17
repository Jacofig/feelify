using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform target;      // miejsce, do którego NPC ma iœæ
    public float speed = 3f;      // prêdkoœæ ruchu
    private bool shouldMove = false;

    void Update()
    {
        if (shouldMove && target != null)
        {
            // oblicz kierunek
            Vector3 dir = (target.position - transform.position).normalized;
            // przesuwaj NPC
            transform.position += dir * speed * Time.deltaTime;

            // opcjonalnie zatrzymaj, gdy dojdziesz do celu
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                shouldMove = false;
            }
        }
    }

    public void MoveToTarget()
    {
        shouldMove = true;
    }
}
