using System;
using Ui;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CharacterSelect
{
    public class CharacterSelectView : MonoBehaviour
    {
        [Inject] private ICharacterSelect characterSelect;
        [SerializeField] private Button selectButton, leftButton, rightButton;
        [SerializeField] private MobileEventObject eventId;

        private void Awake()
        {
            MobileUiEvent.OnSwipeLeft += OnSwipeLeft;
            MobileUiEvent.OnSwipeRight += OnSwipeRight;
            leftButton.onClick.AddListener(LeftButton);
            rightButton.onClick.AddListener(RightButton);
            selectButton.onClick.AddListener(SelectButton);
        }

        private void OnDestroy()
        {
            MobileUiEvent.OnSwipeLeft -= OnSwipeLeft;
            MobileUiEvent.OnSwipeRight -= OnSwipeRight;
        }

        private void Start()
        {
            characterSelect.Init();
        }

        private void OnSwipeRight(MobileEventObject obj)
        {
            if (obj.Id != eventId.Id && !obj) return;
            characterSelect.SwipeRight();
        }

        private void OnSwipeLeft(MobileEventObject obj)
        {
            if (obj.Id != eventId.Id && !obj) return;
            characterSelect.SwipeLeft();
        }
        
        private void RightButton()
        {
            characterSelect.SwipeLeft();
        }

        private void LeftButton()
        {
            characterSelect.SwipeRight();
        }
        
        private void SelectButton()
        {
            characterSelect.Select();
        }
    }
}