using UnityEngine;

namespace Utility
{
    public static class CanvasGroupExtension
    {
        public static void SetActive(this CanvasGroup canvasGroup, bool isActive)
        {
            canvasGroup.alpha = isActive ? 1 : 0;
            canvasGroup.interactable = isActive;
            canvasGroup.blocksRaycasts = isActive;
        }

        public static bool IsActiveSelf(this CanvasGroup canvasGroup)
        {
            return canvasGroup.blocksRaycasts && canvasGroup.interactable && canvasGroup.alpha >= 1;
        }
    }
}