using UnityEngine;
using System.Collections;

public class LevelRotate : MonoBehaviour
{
    public float rotateTime = 0.25f;
    private Rigidbody2D ball;
    private bool rotating = false;

    public void SetBall(Rigidbody2D ballRigidbody)
    {
        ball = ballRigidbody;
    }

    void Update()
    {
        if (rotating) return;
        if (ball == null) return;

        if (ball.linearVelocity.magnitude > 0.05f) return;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(Rotate(-90));
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(Rotate(90));
        }
    }

    IEnumerator Rotate(float angle)
    {
        rotating = true;

        ball.simulated = false;

        Quaternion startRot = transform.rotation;
        Quaternion endRot = startRot * Quaternion.Euler(0, 0, angle);

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / rotateTime;
            transform.rotation = Quaternion.Lerp(startRot, endRot, t);
            yield return null;
        }

        transform.rotation = endRot;

        ball.simulated = true;
        rotating = false;

        // reset flag portalów po pojedynczym obrocie
        ResetPortals();

        // ===== DODANE =====
        // powiadom LevelManager, że wykonano ruch
        LevelManager lm = FindObjectOfType<LevelManager>();
        if (lm != null)
            lm.OnMove();
    }

    void ResetPortals()
    {
        Portal[] portals = FindObjectsOfType<Portal>();
        foreach (var p in portals)
        {
            p.canTeleport = true;
        }
    }
}