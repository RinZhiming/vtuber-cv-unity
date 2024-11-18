using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Main
{
    public class DebugDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform viewport; // Parent canvas or the frame that limits UI movement
        private RectTransform rectTransform;
        
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(viewport, Input.mousePosition, null, out var localPoint);
            rectTransform.localPosition = localPoint;

            Vector3[] corners = new Vector3[4];
            viewport.GetLocalCorners(corners);

            float clampedX = Mathf.Clamp(rectTransform.localPosition.x, corners[0].x + rectTransform.rect.width / 2, corners[2].x - rectTransform.rect.width / 2);
            float clampedY = Mathf.Clamp(rectTransform.localPosition.y, corners[0].y + rectTransform.rect.height / 2, corners[2].y - rectTransform.rect.height / 2);

            rectTransform.localPosition = new Vector3(clampedX, clampedY, rectTransform.localPosition.z);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
        }
    }
}
