using UnityEngine;
using Zenject;

namespace Audio.Microphone
{
    public class MicrophoneInstaller : MonoInstaller
    {
        [SerializeField] private AudioSource audioSource;
        
        public override void InstallBindings()
        {
            Container.Bind<IMicrophone>().To<MicrophoneController>().AsSingle();
            Container.Bind<AudioSource>().FromInstance(audioSource).AsSingle();
            Container.Bind<MicrophoneModel>().AsSingle();
        }
    }
}