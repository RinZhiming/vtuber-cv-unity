using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Head
{
    public class HeadTransformInstaller : MonoInstaller
    {
        [SerializeField] private HeadTransformData headTransformData;
        public override void InstallBindings()
        {
            Container.Bind<IHeadTransformData>().FromInstance(headTransformData).AsSingle().WhenInjectedInto<HeadTransform>();
            Container.Bind<IHeadTransformRunner>().To<HeadTransformController>().AsSingle();
            Container.Bind<HeadTransform>().AsSingle();
            Container.Bind<HeadTransformModel>().AsSingle();
        }
    }
}