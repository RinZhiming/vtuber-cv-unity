using System;
using Audio.AudioEffect;
using UnityEngine.UI;

namespace Audio.AudioPreset
{
    public interface IAudioPresetRunner
    {
        public void Init(ToggleGroup group);
        public void OnDispose();
        public IDisposable SelectToggle(AudioEffectView view, ToggleGroup group);
    }
}