using UnityEngine;
using UnityEngine.UI;

public class ScrollRectWheelOverride : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float pixelsPerWheelTick = 200f;

    void Update()
    {
        float wheel = -Input.mouseScrollDelta.y;

        if (wheel != 0f)
        {
            RectTransform content = scrollRect.content;
            RectTransform viewport = scrollRect.viewport;

            float contentHeight = content.rect.height;
            float viewportHeight = viewport.rect.height;

            if (contentHeight <= viewportHeight)
                return;

            float delta = wheel * pixelsPerWheelTick;
            Vector2 pos = content.anchoredPosition;
            pos.y += delta;

            float maxY = contentHeight - viewportHeight;
            pos.y = Mathf.Clamp(pos.y, 0, maxY);

            content.anchoredPosition = pos;
        }
    }
}
