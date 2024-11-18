using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Head.HeadLookAt
{
    public class HeadLookAtInstaller : MonoInstaller
    {
        [SerializeField] private Transform lookAtTargetTransform;
        [SerializeField] private Transform lookAtRootTransform;
        [SerializeField] private HeadLookAtView headLookAtView;
        
        public override void InstallBindings()
        {
            Container.Bind<IHeadLookAtRunner>().To<HeadLookAtController>().AsSingle();
            Container.Bind<Transform>().WithId("lookAtTarget").FromInstance(lookAtTargetTransform).AsCached();
            Container.Bind<Transform>().WithId("lookAtRoot").FromInstance(lookAtRootTransform).AsCached();
            Container.Bind<IHeadData>().FromInstance(headLookAtView).AsCached().WhenInjectedInto<HeadLookAt>();
            
            Container.Bind<HeadLookAt>().AsSingle();
            Container.Bind<HeadLookAtModel>().AsSingle();
        }
    }
}