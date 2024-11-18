using System;
using Audio.AudioEffect;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Audio.AudioPreset
{
    public class AudioPresetView : MonoBehaviour
    {
        [Inject] private IAudioPresetRunner audioPreset;
        [SerializeField] private AudioEffectView audioEffect;
        [SerializeField] private ToggleGroup group;
        private IDisposable audioPresetDispose;

        private void Awake()
        {
            audioPreset.Init(group);
        }

        private void Start()
        {
            audioPresetDispose = audioPreset.SelectToggle(audioEffect, group);
        }

        private void OnDestroy()
        {
            audioPresetDispose?.Dispose();
        }
    }
}