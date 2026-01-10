using UnityEngine;
using UnityEngine.EventSystems;

public class WindowFocus : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
    }
}
