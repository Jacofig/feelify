using UnityEngine;

public class chase : MonoBehaviour
{
    public float chaseSpeed = 3f;
    public float chaseRange = 5f;

    public Transform player;
    private patrol randomMoveScript;
    private Rigidbody2D rb;

    private Vector2 lastDirection;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        randomMoveScript = GetComponent<patrol>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float dist = Vector2.Distance(rb.position, player.position);

        if (dist < chaseRange)
        {
            randomMoveScript.canMove = false;

            Vector2 dir = ((Vector2)player.position - rb.position).normalized;
            lastDirection = dir;

            rb.MovePosition(rb.position + dir * chaseSpeed * Time.fixedDeltaTime);
        }
        else
        {
            randomMoveScript.canMove = true;
        }
    }


    public Vector2 GetDirection() => lastDirection;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 center = transform.position;
        Gizmos.DrawWireSphere(center, chaseRange);
    }
}
