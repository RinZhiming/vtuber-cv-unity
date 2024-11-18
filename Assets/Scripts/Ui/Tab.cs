using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utility;

namespace Ui
{
    public class Tab : MonoBehaviour
    {
        [SerializeField] private RectTransform tabContainer, stackTabContainer, bodyContainer;
        [SerializeField] private List<TabObject> tabMapper = new();
        [SerializeField] private MobileEventObject eventId;

        private void Awake()
        {
            MobileUiEvent.OnSwipeUp += OnSwipeUp;
            MobileUiEvent.OnSwipeDown += OnSwipeDown;
            tabContainer.sizeDelta = new Vector2(tabContainer.sizeDelta.x, stackTabContainer.sizeDelta.y);

            foreach (var tab in tabMapper)
            {
                tab.TabToggle.onValueChanged.AddListener(b => TabSelect(b, tab));
            }
        }

        private void OnDestroy()
        {
            MobileUiEvent.OnSwipeUp -= OnSwipeUp;
            MobileUiEvent.OnSwipeDown -= OnSwipeDown;
        }

        private void Start()
        {
            tabContainer.sizeDelta = new Vector2(tabContainer.sizeDelta.x, stackTabContainer.sizeDelta.y);
            TabSelect(true, tabMapper[0]);
        }

        private void OnSwipeUp(MobileEventObject uiId)
        {
            if (uiId.Id != eventId.Id) return;
            
            DOVirtual.Vector2(tabContainer.anchorMax, Vector2.one, 0.5f, value => tabContainer.anchorMax = value);
            DOVirtual.Vector2(tabContainer.sizeDelta, Vector2.zero, 0.5f, value =>
            {
                tabContainer.sizeDelta = value;
                bodyContainer.sizeDelta = new Vector2(bodyContainer.sizeDelta.x,
                    tabContainer.rect.height);
            });
            
            Debug.Log("Up");
        }
        
        private void OnSwipeDown(MobileEventObject uiId)
        {
            if (uiId.Id != eventId.Id) return;
            
            DOVirtual.Vector2(tabContainer.anchorMax, new Vector2(1, 0), 0.5f, value => tabContainer.anchorMax = value);
            DOVirtual.Vector2(tabContainer.sizeDelta, new Vector2(tabContainer.sizeDelta.x, stackTabContainer.sizeDelta.y), 0.5f, value =>
            {
                tabContainer.sizeDelta = value;
            });
            
            Debug.Log("Down");
        }
        
        private void TabSelect(bool isSelect, TabObject tab)
        {
            tab.Tab.SetActive(isSelect);
        }
    }

    [Serializable]
    public struct TabObject
    {
        [SerializeField] private CanvasGroup tab;
        [SerializeField] private Toggle tabToggle;

        public CanvasGroup Tab
        {
            get => tab;
            set => tab = value;
        }

        public Toggle TabToggle
        {
            get => tabToggle;
            set => tabToggle = value;
        }
    }
}
