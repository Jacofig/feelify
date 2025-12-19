using UnityEngine;

[ExecuteAlways]
public class ParticleScaler : MonoBehaviour
{
    public RectTransform canvasRect;
    private Vector3 initialScale;
    private Vector2 initialCanvasSize;

    void Start()
    {
        if (!canvasRect) canvasRect = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
        initialScale = transform.localScale;
        initialCanvasSize = canvasRect.sizeDelta;
    }

    void Update()
    {
        Vector2 currentSize = canvasRect.sizeDelta;
        float scaleFactor = currentSize.x / initialCanvasSize.x; // proporcjonalnie do szerokoœci
        transform.localScale = initialScale * scaleFactor;
    }
}
