using System;
using System.Linq;
using Audio.AudioEffect;
using R3;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using Zenject;

namespace Audio.AudioPreset
{
    public class AudioPresetController : IAudioPresetRunner
    {
        [Inject] private AudioPresetModel model;

        public void Init(ToggleGroup group)
        {
            AudioEffectView.OnValueChange += OnAudioValueChange;
            
            var save = PlayerPrefs.GetInt(SaveState.AudioPreset);

            if (save == -1)
            {
                group.GetToggleByChildrenIndex(0).isOn = true;
                return;
            }
            
            group.GetToggleByChildrenIndex(save).isOn = true;
        }

        public void OnDispose()
        {
            AudioEffectView.OnValueChange -= OnAudioValueChange;
        }

        private void OnAudioValueChange()
        {
            model.CustomToggle.isOn = true;
        }

        public IDisposable SelectToggle(AudioEffectView view, ToggleGroup group)
        {
            return Observable.EveryValueChanged(group, toggleGroup => toggleGroup.GetFirstActiveToggle()).Subscribe(t =>
            {
                SelectAudioPreset(t, view, group);
            });
        }

        private void SelectAudioPreset(Toggle toggle, AudioEffectView view, ToggleGroup group)
        {
            var index = group.GetIndexByChildrenToggle(toggle);

            if (index != -1)
            {
                PlayerPrefs.SetInt(SaveState.AudioPreset, index);
                PlayerPrefs.Save();
            } 
            
            
            if (toggle == model.CustomToggle) return;
            
            foreach (var audioPreset in model.AudioPresets)
            {
                if (audioPreset.Toggle == toggle) SetAudioPreset(audioPreset.AudioEffectData, view);
            }
        }

        private void SetAudioPreset(AudioEffectData data, AudioEffectView view)
        {
            view.SetAudioData(data);
        }
    }
}