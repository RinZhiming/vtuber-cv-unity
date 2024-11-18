using OpenCVForUnity.UnityUtils.Helper;
using UnityEngine;
using Zenject;

namespace Webcam
{
    public class WebcamInstaller : MonoInstaller
    {
        [SerializeField] private WebCamTextureToMatHelper webCamTextureToMatHelper;
        [SerializeField] private ImageOptimizationHelper imageOptimizationHelper;
        
        public override void InstallBindings()
        {
            Container.Bind<WebCamTextureToMatHelper>().FromInstance(webCamTextureToMatHelper).AsSingle();
            Container.Bind<ImageOptimizationHelper>().FromInstance(imageOptimizationHelper).AsSingle();

            Container.Bind<IWebcamRunner>().To<WebcamController>().AsSingle();

            Container.Bind<WebcamDetect>().AsSingle();
            Container.Bind<WebcamModel>().AsSingle();
        }
    }
}