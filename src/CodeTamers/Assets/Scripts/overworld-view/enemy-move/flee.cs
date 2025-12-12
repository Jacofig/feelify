using UnityEngine;

public class flee : MonoBehaviour
{
    public float fleeSpeed = 3f;
    public float fleeRange = 5f;
    public float teleportDistance = 10f; // odleg³oœæ, po której teleportujemy
    public Transform spawnPoint; // punkt wyjœcia, do którego teleportujemy

    public Transform player;
    
    private Rigidbody2D rb;

    private Vector2 lastDirection;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        rb = GetComponent<Rigidbody2D>();

        // jeœli nie przypisano spawnPoint w inspectorze, ustawiamy na pocz¹tkow¹ pozycjê
        if (spawnPoint == null)
        {
            GameObject sp = new GameObject("SpawnPoint");
            sp.transform.position = transform.position;
            spawnPoint = sp.transform;
        }
    }

    void FixedUpdate()
    {
        float dist = Vector2.Distance(rb.position, player.position);

        if (dist < fleeRange)
        {
           

            // Kierunek ucieczki: odwrócony wektor od gracza
            Vector2 dir = (rb.position - (Vector2)player.position).normalized;
            lastDirection = dir;

            rb.MovePosition(rb.position + dir * fleeSpeed * Time.fixedDeltaTime);
        }
        

        // Teleportacja jeœli gracz jest zbyt daleko
        if (dist > teleportDistance)
        {
            rb.position = spawnPoint.position;
        }
    }

    public Vector2 GetDirection() => lastDirection;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 center = transform.position;
        Gizmos.DrawWireSphere(center, fleeRange);

        Gizmos.color = Color.red;
        if (spawnPoint != null)
            Gizmos.DrawWireSphere(spawnPoint.position, 0.3f);
    }
}
