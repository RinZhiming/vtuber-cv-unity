using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SoundEffect
{
    public class SoundEffectInstaller : MonoInstaller
    {
        [SerializeField] private SoundEffect[] soundEffects;
            public override void InstallBindings()
            {
                Container.Bind<SoundEffect[]>().FromInstance(soundEffects).AsSingle().WhenInjectedInto<SoundEffectModel>();
                Container.Bind<SoundEffectModel>().AsSingle();
                Container.Bind<ISoundEffectRunner>().To<SoundEffectController>().AsSingle();
            }
    }

    [Serializable]
    public class SoundEffect
    {
        [SerializeField] private Button button;
        [SerializeField] private AudioClip clip;

        public Button Button => button;
        public AudioClip Clip => clip;
    }
}

