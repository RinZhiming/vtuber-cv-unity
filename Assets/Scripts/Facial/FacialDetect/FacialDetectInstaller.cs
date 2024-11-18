using Facial.FacialCalculate;
using OpenCVForUnity.UnityUtils.Helper;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Facial.FacialDetect
{
    public class FacialDetectInstaller : MonoInstaller
    {
        [SerializeField] private RawImage screen;
        
        public override void InstallBindings()
        {
            Container.Bind<IFacialDetectRunner>().To<FacialDetectController>().AsSingle();
            Container.Bind<FacialDetectModel>().AsSingle();
            Container.Bind<FacialCalculator>().AsSingle();
            
            Container.Bind<RawImage>().FromInstance(screen).AsSingle().WhenInjectedInto<FacialDetector>();
            Container.Bind<FacialDetector>().AsSingle();
        }
    }
}