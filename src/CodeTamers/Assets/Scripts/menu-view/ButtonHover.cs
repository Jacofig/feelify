using UnityEngine;
using UnityEngine.EventSystems; // potrzebne do IPointerEnter/Exit

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 normalScale = Vector3.one;
    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1f);
    public float smooth = 5f;

    private void Update()
    {
        // Smooth scale transition
        transform.localScale = Vector3.Lerp(transform.localScale, normalScale, Time.deltaTime * smooth);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        normalScale = hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        normalScale = Vector3.one;
    }
}
