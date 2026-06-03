using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltipPanel;

    private void Start()
    {
        tooltipPanel.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipPanel.SetActive(false);
    }
}