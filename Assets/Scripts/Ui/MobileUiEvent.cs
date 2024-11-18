using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui
{
    public enum EventType
    {
        VerticalSwipe,
        HorizontalSwipe,
        Click,
    }
    public class MobileUiEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,IDragHandler
    {
        public static Action<MobileEventObject> OnSwipeUp;
        public static Action<MobileEventObject> OnSwipeDown;
        
        public static Action<MobileEventObject> OnSwipeLeft;
        public static Action<MobileEventObject> OnSwipeRight;
        
        public static Action<MobileEventObject> OnClick;
        
        [SerializeField] private EventType eventType;
        [SerializeField] private float dragThreshold;
        [SerializeField] private MobileEventObject id;
        private Vector2 startPosition, currentPosition;
        private bool isTouch;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            switch (eventType)
            {
                case EventType.Click:
                    OnClick?.Invoke(id);
                    break;
                case EventType.VerticalSwipe:
                    startPosition = eventData.position;
                    if (!isTouch) 
                        isTouch = true;
                    break;
                case EventType.HorizontalSwipe:
                    startPosition = eventData.position;
                    if (!isTouch) 
                        isTouch = true;
                    break;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (isTouch)
            {
                currentPosition = eventData.position;
                var distance = currentPosition - startPosition;

                if (eventType == EventType.VerticalSwipe)
                {
                    if (Mathf.Abs(distance.y) > dragThreshold)
                    {
                        if (distance.y > 0)
                        {
                            OnSwipeUp?.Invoke(id);
                        }
                        else
                        {
                            OnSwipeDown?.Invoke(id);
                        }
                    }
                }

                if (eventType == EventType.HorizontalSwipe)
                {
                    if (Mathf.Abs(distance.x) > dragThreshold)
                    {
                        if (distance.x < 0)
                        {
                            OnSwipeLeft?.Invoke(id);
                        }
                        else
                        {
                            OnSwipeRight?.Invoke(id);
                        }
                    }
                }

                isTouch = false;
                StartCoroutine(DelayTouch());
            }
            
        }

        private IEnumerator DelayTouch()
        {
            yield return new WaitForSeconds(0.5f);
            isTouch = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if(eventType != EventType.Click) isTouch = false;
        }
    }
}
