using System;
using Audio.AudioEffect;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Audio.AudioPreset
{
    public class AudioPresetInstaller : MonoInstaller
    {
        [SerializeField] private AudioPreset[] audioPresets;
        [SerializeField] private Toggle customToggle;
        public override void InstallBindings()
        {
            Container.Bind<AudioPreset[]>().FromInstance(audioPresets).AsSingle().WhenInjectedInto<AudioPresetModel>();
            Container.Bind<Toggle>().FromInstance(customToggle).AsSingle().WhenInjectedInto<AudioPresetModel>();
            Container.Bind<IAudioPresetRunner>().To<AudioPresetController>().AsSingle();
            Container.Bind<AudioPresetModel>().AsSingle();
        }
    }

    [Serializable]
    public class AudioPreset
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private AudioEffectData audioEffectData;

        public Toggle Toggle => toggle;
        public AudioEffectData AudioEffectData => audioEffectData;
    }
}