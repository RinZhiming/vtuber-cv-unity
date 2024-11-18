using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Main
{
    public class MainView : MonoBehaviour
    {
        [Inject] private IMainRunner main;
        [SerializeField] private Button cameraButton, hideCameraButton, tabButton, hideTabButton;
        [SerializeField] private CanvasGroup cameraGroup, tabGroup;

        private void Awake()
        {
            main.Init();
            main.Camera(cameraButton,hideCameraButton, cameraGroup);
            main.Tab(tabButton, hideTabButton, tabGroup);
        }
    }
}