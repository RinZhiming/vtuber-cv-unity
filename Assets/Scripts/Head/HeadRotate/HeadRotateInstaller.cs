using UnityEngine;
using Zenject;

namespace Head.HeadRotate
{
    public class HeadRotateInstaller : MonoInstaller
    {
        [SerializeField] private Transform headTransform;
        [SerializeField] private HeadRotateView headRotateView;
        public override void InstallBindings()
        {
            Container.Bind<Transform>().FromInstance(headTransform).AsCached().WhenInjectedInto<HeadRotate>();
            Container.Bind<IHeadData>().FromInstance(headRotateView).AsCached().WhenInjectedInto<HeadRotate>();
            Container.Bind<IHeadRotateRunner>().To<HeadRotateController>().AsSingle();
            Container.Bind<HeadRotate>().AsSingle();
            Container.Bind<HeadRotateModel>().AsSingle();
        }
    }
}