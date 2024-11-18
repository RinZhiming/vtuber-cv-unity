using UnityEngine;
using Zenject;

namespace Facial.FacialMovement
{
    public class FacialMovementController : IFacialMovementRunner
    {
        [Inject] private FacialMovementModel model;

        public void UpdateMapper()
        {
            model.FacialMovementMapper.UpdateMapper();
        }

        public void Map()
        {
            model.FacialMovementMapper.Map();
        }
    }
}