using Audio.AudioEffect;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class AudioEffectInstaller : MonoInstaller
{
    [SerializeField] private AudioMixer mixer;
    public override void InstallBindings()
    {
        Container.Bind<AudioMixer>().FromInstance(mixer).AsSingle();
        Container.Bind<IAudioEffect>().To<AudioEffectController>().AsSingle();
        
        Container.Bind<AudioEffectModel>().AsSingle();
    }
}