using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Background
{
    public class BackgroundView : MonoBehaviour
    {
        [Inject] private IBackgroundRunner background;
        [SerializeField] private ToggleGroup toggleGroup;
        [SerializeField] private Image backgroundImage;
        private IDisposable backgroundDispose;

        private void Awake()
        {
            background.Init(toggleGroup);
        }

        private void Start()
        {
            backgroundDispose = background.SelectToggle(toggleGroup, backgroundImage);
        }

        private void OnDestroy()
        {
            backgroundDispose?.Dispose();
        }
    }
}
