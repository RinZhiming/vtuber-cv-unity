using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Webcam
{
    public class WebcamView : MonoBehaviour
    {
        [SerializeField] private Button switchButton;
        [Inject] private IWebcamRunner webcam;

        private void Awake()
        {
            switchButton.onClick.AddListener(SwitchButton);
        }

        private void OnDestroy()
        {
            webcam.Dispose();
        }

        private void SwitchButton()
        {
            webcam.Switch();
        }
    }
}