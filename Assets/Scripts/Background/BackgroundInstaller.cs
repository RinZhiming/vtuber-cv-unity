using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Background
{
    public class BackgroundInstaller : MonoInstaller
    {
        [SerializeField] private Background[] backgrounds;
        
        public override void InstallBindings()
        {
            Container.Bind<BackgroundModel>().AsSingle();
            Container.Bind<IBackgroundRunner>().To<BackgroundController>().AsSingle();
            Container.Bind<Background[]>().FromInstance(backgrounds).WhenInjectedInto<BackgroundModel>();
        }
    }

    [Serializable]
    public class Background
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private Sprite spriteBackground;

        public Toggle Toggle => toggle;
        public Sprite SpriteBackground => spriteBackground;
    }
}