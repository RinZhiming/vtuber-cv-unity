using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Main
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private MainTab[] mainTabs;
        public override void InstallBindings()
        {
            Container.Bind<MainTab[]>().FromInstance(mainTabs).AsSingle().WhenInjectedInto<MainModel>();
            Container.Bind<MainModel>().AsSingle();
            Container.Bind<IMainRunner>().To<MainController>().AsSingle();
        }
    }

    [Serializable]
    public class MainTab
    {
        [SerializeField] private Button button;
        [SerializeField] private CanvasGroup group;

        public Button Button => button;
        public CanvasGroup Group => group;
    }
}