using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility;
using Zenject;

namespace Audio.Microphone
{
    public class MicrophoneView : MonoBehaviour
    {
        [Inject] private IMicrophone microphone;
        [SerializeField] private Toggle muteToggle;
        [SerializeField] private Sprite muteIcon, unMuteIcon;
        [SerializeField] private Slider sliderAudio;
        //[SerializeField] private TextMeshProUGUI deviceNameText;
        [SerializeField] private Button requestPermissionButton;
        [SerializeField] private CanvasGroup requestFailGroup, headphoneNotfoundGroup;
        private IDisposable headphoneDetectDispose;

        private void Awake()
        {
            requestPermissionButton.onClick.AddListener(RequestPermissionButton);
            muteToggle.onValueChanged.AddListener(OnMuteToggle);
        }

        private void Start()
        {
            headphoneDetectDispose = microphone.HeadphoneDetection(HeadphoneDetect);
            requestFailGroup.SetActive(true);
            RequestMicrophone();
        }

        private async void RequestMicrophone()
        {
            var sceneContext = FindObjectOfType<SceneContext>();

            await UniTask.WaitUntil(() => sceneContext.Initialized);
            
            if (Permission.HasUserAuthorizedPermission(Permission.Microphone))
            {
                requestFailGroup.SetActive(false);
                microphone.Begin();
            }
        }
        
        private void OnMuteToggle(bool mute)
        {
            muteToggle.targetGraphic.GetComponent<Image>().sprite = mute ? muteIcon : unMuteIcon;
            microphone.Mute(mute);
        }

        private void OnDestroy()
        {
            headphoneDetectDispose?.Dispose();
            microphone.End();
        }

        private void Update()
        {
            sliderAudio.value = microphone.SpectreData();
            //deviceNameText.text = microphone.GetDeviceName();
        }

        private void RequestPermissionButton()
        {
            SceneManager.LoadScene(Scenes.RunScene);
        }

        private void HeadphoneDetect(bool detect)
        {
            muteToggle.isOn = detect;
            OnMuteToggle(detect);
            headphoneNotfoundGroup.SetActive(detect);
        }
    }
}