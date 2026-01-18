using UnityEngine;
using UnityEngine.AI;
using System;

public class NPCMovement : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    private Action onReachedTarget;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (agent.hasPath && !agent.pathPending)
        {
            // sprawdzamy czy agent prawie dotarł
            if (Vector3.Distance(transform.position, agent.destination) < 0.1f)
            {
                agent.ResetPath();
                onReachedTarget?.Invoke();
                onReachedTarget = null; // żeby callback był wywołany tylko raz
            }
        }
    }

    // teraz przyjmujemy callback
    public void MoveToTarget(Action onFinished = null)
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            onReachedTarget = onFinished;
        }
        else
        {
            // jeśli brak celu → od razu zakończ
            onFinished?.Invoke();
        }
    }
}
