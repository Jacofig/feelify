using UnityEngine;
using UnityEngine.UI;

public class ForgeResultController : MonoBehaviour
{
    [SerializeField] private Image image;

    [Header("Sprites")]
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failedSprite;
    [SerializeField] private Sprite hitColdSprite;

    [SerializeField] private ForgeManager forgeManager;

    void Awake()
    {
        forgeManager.OnForgeResult += OnForgeResult;
        image.sprite = idleSprite;
    }

    void OnDestroy()
    {
        forgeManager.OnForgeResult -= OnForgeResult;
    }

    private void OnForgeResult(ForgeResultType result)
    {
        switch (result)
        {
            case ForgeResultType.Success:
                image.sprite = successSprite;
                break;

            case ForgeResultType.HitColdMetal:
                image.sprite = hitColdSprite;
                break;

            case ForgeResultType.CodeError:
            case ForgeResultType.Failed:
                image.sprite = failedSprite;
                break;
        }
    }
}
