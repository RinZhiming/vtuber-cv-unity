using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gesture
{
    public class GestureInstaller : MonoInstaller
    {
        [SerializeField] private Gesture[] gestures;
        public override void InstallBindings()
        {
            Container.Bind<Gesture[]>().FromInstance(gestures).AsSingle().WhenInjectedInto<GestureModel>();
            Container.Bind<GestureModel>().AsSingle();
            Container.Bind<IGestureRunner>().To<GestureController>().AsSingle();
        }
    }

    [Serializable]
    public class Gesture
    {
        [SerializeField] private Button button;
        [SerializeField] private string name;
        [SerializeField] private AnimationClip animation;
        [SerializeField] private Sprite press, notPress;

        public Button Button => button;
        public string Name => name;
        public AnimationClip Animation => animation;
        public Sprite Press => press;
        public Sprite NotPress => notPress;
    }
}