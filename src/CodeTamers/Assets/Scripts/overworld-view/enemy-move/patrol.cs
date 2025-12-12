using System.Diagnostics;
using System;
using UnityEngine;
//using static System.Net.Mime.MediaTypeNames;

public class patrol : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float changeDirTime = 2f;
    public float moveRadius = 5f; // promień strefy patrolu

   

    private Vector2 direction;
    private float timer;
    private Vector2 startPos;

    public bool canMove = true;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = rb.position; // używamy pozycji Rigidbody
        ChooseNewDirection();
    }

    void FixedUpdate() // zmieniamy Update na FixedUpdate dla fizyki
    {

        if (!canMove) return;

        timer -= Time.fixedDeltaTime;
        if (timer <= 0)
        {
            if (UnityEngine.Random.value < 0.3f)
                direction = Vector2.zero; // czasem się zatrzyma
            else
                ChooseNewDirection();

            timer = changeDirTime;
        }
       


        // ruch przeciwnika przy użyciu MovePosition
        Vector2 newPos = rb.position + direction * moveSpeed * Time.fixedDeltaTime;
        Vector2 offset = newPos - startPos;

        // jeśli wychodzi poza koło → zawraca
        if (offset.magnitude > moveRadius)
        {
            Vector2 backToCenter = -offset.normalized;
            direction = (backToCenter + UnityEngine.Random.insideUnitCircle * 0.3f).normalized;

            // cofamy przeciwnika na krawędź koła
            newPos = startPos + offset.normalized * moveRadius;
        }

        rb.MovePosition(newPos);
    }

    void ChooseNewDirection()
    {
        direction = UnityEngine.Random.insideUnitCircle.normalized;
    }

    public Vector2 GetDirection() => direction;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector2 center = Application.isPlaying ? startPos : (Vector2)transform.position;
        Gizmos.DrawWireSphere(center, moveRadius);
    }
}
