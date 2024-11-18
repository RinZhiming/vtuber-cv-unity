using UnityEngine;
using Zenject;

namespace Facial.FacialMovement
{
    public class FacialMovementInstaller : MonoInstaller
    {
        [SerializeField] private FacialMovementView facialView;
        
        public override void InstallBindings()
        {
            Container.Bind<IFacialMovementData>().FromInstance(facialView).AsSingle();
            Container.Bind<FacialMovementMapper>().AsSingle();
            
            Container.Bind<IFacialMovementRunner>().To<FacialMovementController>().AsSingle();
            Container.Bind<FacialMovementModel>().AsSingle();

        }
    }
}